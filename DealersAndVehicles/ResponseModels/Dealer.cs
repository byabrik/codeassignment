using Newtonsoft.Json;
using System.Collections.Generic;

namespace DealersAndVehicles
{
    public class DealerResponseModel
    {
        [JsonProperty("dealerId")]
        public int DealerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("vehicles")]
        public List<VehicleResponseModel> Vehicles { get; set; }
    }
}
