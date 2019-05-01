using Newtonsoft.Json;

namespace DealersAndVehicles
{
    public class VehicleResponseModel
    {
        [JsonProperty("vehicleId")]
        public int VehicleId { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }
    }
}
