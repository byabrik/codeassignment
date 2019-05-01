using Newtonsoft.Json;
using System.Collections.Generic;

namespace DealersAndVehicles
{
    public class VehiclesRequestModel : IApiError
    {
        [JsonProperty("vehicleIds")]
        public List<int> VehicleIds { get; set; }
        public string ErrorMessage { get; set; }
    }
}
