using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DealersAndVehicles
{
    /// <summary>
    /// This class manages an API requests neded to complete assignment
    /// </summary>
    public class ApiExplorer
    {
        private readonly ApiClient _apiClient;

        public ApiExplorer(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<(DataSetResponseModel responseModel, string datasetId)> GetDealersAndVehicles()
        {
            // get datasetId
            var dataset = await _apiClient.Get<DataSetRequestModel>("/datasetId");
            if (dataset == null || string.IsNullOrWhiteSpace(dataset.DatasetId))
                throw new ApiException("Dataset could not be found");

            // get vehicles 
            var vehicles = await _apiClient.Get<VehiclesRequestModel>($"/{dataset.DatasetId}/vehicles");
            if (vehicles == null)
                throw new ApiException("Could not retrieve vehicle ids");

            // get data for each vehicle
            var vehicleData = new ConcurrentBag<VehicleRequestModel>();
            await vehicles.VehicleIds.ForEachAsync(vehicles.VehicleIds.Count, async vId =>
                vehicleData.Add(await _apiClient.Get<VehicleRequestModel>($"/{dataset.DatasetId}/vehicles/{vId}")));
            if (vehicleData == null)
                throw new ApiException("Could not retrieve vehicle information");

            // get dealer ids
            var dealerIds = vehicleData.GroupBy(v => v.DealerId).Select(grp => grp.Key).ToList();
            
            // get dealers data
            var dealers = new ConcurrentBag<DealerRequestModel>();
            await dealerIds.ForEachAsync(dealerIds.Count, async dealerId =>
                 dealers.Add(await _apiClient.Get<DealerRequestModel>($"/{dataset.DatasetId}/dealers/{dealerId}")));

            var response = new DataSetResponseModel
            {
                Dealers = dealers.Select(d =>
                                    new DealerResponseModel
                                    {
                                        DealerId = d.DealerId,
                                        Name = d.Name,
                                        Vehicles = vehicleData.Where(v => v.DealerId == d.DealerId)
                                                              .Select(v =>
                                                                    new VehicleResponseModel
                                                                    {
                                                                        Make = v.Make,
                                                                        Model = v.Model,
                                                                        VehicleId = v.VehicleId,
                                                                        Year = v.Year
                                                                    })
                                                               .ToList()
                                    })
                                    .OrderBy(d=> d.DealerId)
                                    .ToList()
            };

            return (response, dataset.DatasetId);
        }

        public async Task<AnswerResponse> PostAnswer(string datasetId, DataSetResponseModel response)
        {
            return await _apiClient.Post<AnswerResponse>($"/{datasetId}/answer", response);
        }
    }
}
