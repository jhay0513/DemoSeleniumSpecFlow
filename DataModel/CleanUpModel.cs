using Newtonsoft.Json;
using System.Collections.Generic;

namespace DemoSeleniumSpecFlow.DataModel
{
    public class CleanUpModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

    }

}
