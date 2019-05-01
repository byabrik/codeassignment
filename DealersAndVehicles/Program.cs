using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DealersAndVehicles
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string apiBaseUrl = "http://api.coxauto-interview.com/api";

            var httpClientWrapper = new HttpClientWrapper();
            var apiClient = new ApiClient(httpClientWrapper, apiBaseUrl);
            var apiExplorer = new ApiExplorer(apiClient);
            (var responseModel, var datasetId) =  await apiExplorer.GetDealersAndVehicles();
            var response = await apiExplorer.PostAnswer(datasetId, responseModel);

            Console.WriteLine(JsonConvert.SerializeObject(response, Formatting.Indented));
        }
    }
}
