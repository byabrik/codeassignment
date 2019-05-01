using Newtonsoft.Json;
using System.Collections.Generic;

namespace DealersAndVehicles
{
    public class DataSetResponseModel
    {
        [JsonProperty("dealers")]
        public List<DealerResponseModel> Dealers { get; set; }
    }
}