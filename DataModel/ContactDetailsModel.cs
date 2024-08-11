using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSeleniumSpecFlow.DataModel
{
    public class ContactDetailsModel
    {
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("DateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("StreetAddress1")]
        public string StreetAddress1 { get; set; }

        [JsonProperty("StreetAddress2")]
        public string StreetAddress2 { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("StateOrProvince")]
        public string StateOrProvince { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }
    }
}
