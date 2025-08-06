import axios from 'axios';

// Create axios instances for different API services
export const identityApi = axios.create({
  baseURL: import.meta.env.VITE_IDENTITY_API_URL || '',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const frontOfficeApi = axios.create({
  baseURL: import.meta.env.VITE_FRONT_OFFICE_API_URL || '',
  headers: {
    'Content-Type': 'application/json',
  },
});

export const backOfficeApi = axios.create({
  baseURL: import.meta.env.VITE_BACK_OFFICE_API_URL || '',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Add authorization token to requests
const addAuthToken = (token: string | null) => {
  if (token) {
    identityApi.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    frontOfficeApi.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    backOfficeApi.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  } else {
    delete identityApi.defaults.headers.common['Authorization'];
    delete frontOfficeApi.defaults.headers.common['Authorization'];
    delete backOfficeApi.defaults.headers.common['Authorization'];
  }
};

// Handle 401 responses with refresh token
const setup401Interceptor = (
  refreshToken: string | null, 
  onRefreshSuccess: (token: string, refreshToken: string) => void,
  onRefreshFailed: () => void
) => {
  const responseInterceptor = (client: typeof identityApi) => {
    client.interceptors.response.use(
      (response) => response,
      async (error) => {
        const originalRequest = error.config;

        // If the error is 401 and we haven't tried to refresh the token yet
        if (
          error.response?.status === 401 &&
          !originalRequest._retry &&
          refreshToken
        ) {
          originalRequest._retry = true;

          try {
            // Attempt to refresh the token
            const response = await identityApi.post('/connect/refresh', {
              refreshToken,
            });

            const { token, refreshToken: newRefreshToken } = response.data;
            
            // Update the tokens in the system
            onRefreshSuccess(token, newRefreshToken);
            
            // Update the Authorization header
            originalRequest.headers['Authorization'] = `Bearer ${token}`;
            
            // Retry the original request
            return client(originalRequest);
          } catch (error) {
            // If refresh token fails, logout the user
            onRefreshFailed();
            return Promise.reject(error);
          }
        }

        return Promise.reject(error);
      }
    );
  };

  // Apply interceptor to all API instances only once
  [identityApi, frontOfficeApi, backOfficeApi].forEach((apiInstance) => {
    responseInterceptor(apiInstance);
  });
};

export { addAuthToken, setup401Interceptor, axios };