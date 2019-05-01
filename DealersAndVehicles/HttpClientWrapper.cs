using System.Net.Http;
using System.Threading.Tasks;

namespace DealersAndVehicles
{
    /// <summary>
    /// HttpClientWrapper is used to ensure reusability of the HttpClient and testability of the client
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        public HttpClient Client { get; }

        public HttpClientWrapper()
        {
            Client = new HttpClient(new HttpClientHandler { UseCookies = false, AllowAutoRedirect = false, MaxConnectionsPerServer = int.MaxValue });
        }

        public Task<HttpResponseMessage> GetAsync(string uri)
        {
            return Client.GetAsync(uri);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return Client.SendAsync(request);
        }
    }
}