import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.tsx';
import './index.css';
import './i18n';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';

// Initialize MSW in development
async function initMSW() {
  if (import.meta.env.DEV) {
    const { worker } = await import('./mocks/browser');
    return worker.start({
      onUnhandledRequest: 'bypass', // Don't warn about unhandled requests
    });
  }
  return Promise.resolve();
}
const queryClient = new QueryClient()
// Bootstrap the application
initMSW().then(() => {
  createRoot(document.getElementById('root')!).render(
    <StrictMode>
     <QueryClientProvider client={queryClient}>
      <App />
    </QueryClientProvider>
    </StrictMode>
  );
});