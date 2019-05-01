using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DealersAndVehicles
{
    /// <summary>
    /// Http Api Client that is used to make http request to an api hosted on baseUrl
    /// Requires IHttpClientWrapper to ensure testability
    /// </summary>
    public class ApiClient
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly string _baseUrl;

        public ApiClient(IHttpClientWrapper httpClientWrapper, string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClientWrapper = httpClientWrapper;
        }

        /// <summary>
        /// Makes a GET request to an API endpoint located on <baseUrl>/url
        /// </summary>
        /// <param name="url">An API endpoint url</param>
        /// <returns>T serialized from the response message</returns>
        public async Task<T> Get<T>(string url)
            where T : IApiError
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}{url}");

            using (var response = await _httpClientWrapper.SendAsync(request))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(jsonResponse);
                if (response.IsSuccessStatusCode) return data;

                HandleError(response, data);

                return data;
            }
        }

        /// <summary>
        /// Makes a POST request to an API endpoint located on <baseUrl>/url
        /// </summary>
        /// <param name="url">An API endpoint url</param>
        /// <returns>T serialized from the response message</returns>
        public async Task<T> Post<T>(string url, object body)
             where T : IApiError
        {
            var requestUrl = $"{_baseUrl}{url}";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var content = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            using (var response = await _httpClientWrapper.SendAsync(request))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<T>(jsonResponse);
                if (response.IsSuccessStatusCode) return data;

                HandleError(response, data);

                return data;
            }
        }

        private static void HandleError<T>(HttpResponseMessage response, T data) where T : IApiError
        {
            var sb = new StringBuilder();
            sb.AppendLine(response.ReasonPhrase);
            foreach (var header in response.Headers)
                foreach (var val in header.Value)
                    sb.AppendFormat("{0}: {1}\n", header.Key, val);

            sb.Append($"Error Message:{data.ErrorMessage}");
            ((IApiError)data).ErrorMessage = sb.ToString();
        }
    }
}