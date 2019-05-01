using Newtonsoft.Json;

namespace DealersAndVehicles
{
    public class VehicleRequestModel : IApiError
    {
        [JsonProperty("vehicleId")]
        public int VehicleId { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("dealerId")]
        public int DealerId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
