using Microsoft.AspNetCore.Http;


namespace SchoolSaas.Infrastructure.Backoffice.Helper
{
    public static class HttlpClientExtension
    {

        public static async Task<R> PostAsync<R>(this HttpClient httpClient, object data, string path, CancellationToken cancellationToken)
        {


            if (data == null)
                throw new ArgumentNullException("data");

            var _baseUrl = httpClient.BaseAddress.AbsoluteUri;
            var client_ = httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    string json_ = System.Text.Json.JsonSerializer.Serialize(data);
                    var content_ = new StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod(HttpMethods.Post);
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "registrationrequest/addregistrationrequest"
                    urlBuilder_.Append(path);


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new Dictionary<string, IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<R>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        public static async Task<R> PutAsync<R>(this HttpClient httpClient, object data, string path, CancellationToken cancellationToken)
        {


            if (data == null)
                throw new ArgumentNullException("data");

            var _baseUrl = httpClient.BaseAddress.AbsoluteUri;
            var client_ = httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    string json_ = System.Text.Json.JsonSerializer.Serialize(data);
                    var content_ = new StringContent(json_);
                    content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod(HttpMethods.Put);
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                    // Operation Path: "registrationrequest/addregistrationrequest"
                    urlBuilder_.Append(path);


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new Dictionary<string, IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<R>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }


        public static async Task<R> GetAsync<R>(this HttpClient httpClient,object param,string path, CancellationToken cancellationToken)
        {
            var client_ = httpClient;
            var disposeClient_ = false;
            var _baseUrl = httpClient.BaseAddress.AbsoluteUri;

            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    var urlBuilder_ = new System.Text.StringBuilder();
                    if (!string.IsNullOrEmpty(_baseUrl)) urlBuilder_.Append(_baseUrl);
                   
                    urlBuilder_.Append(path);

                    if (param != null)
                    {
                        var queryString = ObjectToQueryString(param);
                        if (!string.IsNullOrEmpty(queryString))
                        {
                            if (urlBuilder_.ToString().Contains("?"))
                                urlBuilder_.Append("&");
                            else
                                urlBuilder_.Append("?");
                            urlBuilder_.Append(queryString);
                        }
                    }


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = new Dictionary<string, IEnumerable<string>>();
                        foreach (var item_ in response_.Headers)
                            headers_[item_.Key] = item_.Value;
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<R>(response_, headers_, cancellationToken).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        private static string ObjectToQueryString(object obj)
        {
            if (obj == null)
                return string.Empty;

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + Uri.EscapeDataString(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
        private struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                Object = responseObject;
                Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }


        private static async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response,IReadOnlyDictionary<string, IEnumerable<string>> headers, CancellationToken cancellationToken)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default, string.Empty);
            }


            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                var typedBody = System.Text.Json.JsonSerializer.Deserialize<T>(responseText);

                return new ObjectResponseResult<T>(typedBody, responseText);
            }
            catch (Newtonsoft.Json.JsonException exception)
            {
                var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
            }

        }

        public partial class ApiException : Exception
        {
            public int StatusCode { get; private set; }

            public string Response { get; private set; }

            public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

            public ApiException(string message, int statusCode, string response,IReadOnlyDictionary<string, IEnumerable<string>> headers, Exception innerException)
                : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + (response == null ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
            {
                StatusCode = statusCode;
                Response = response;
                Headers = headers;
            }

            public override string ToString()
            {
                return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
            }
        }

    }

}
