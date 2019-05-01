using System.Net.Http;
using System.Threading.Tasks;

namespace DealersAndVehicles
{
    public interface IHttpClientWrapper
    {
        HttpClient Client { get; }
        Task<HttpResponseMessage> GetAsync(string uri);
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}