using Newtonsoft.Json;
using System.Collections.Generic;

namespace DealersAndVehicles
{
    public class DataSetRequestModel : IApiError
    {
        [JsonProperty("datasetId")]
        public string DatasetId { get; set; }

        public string ErrorMessage { get; set; }
    }
}