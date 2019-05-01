using Newtonsoft.Json;
using System.Collections.Generic;

namespace DealersAndVehicles
{
    public class DealerRequestModel : IApiError
    {
        [JsonProperty("dealerId")]
        public int DealerId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public string ErrorMessage { get; set; }
    }
}
