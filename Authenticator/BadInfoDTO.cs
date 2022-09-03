using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authenticator
{
    public class BadInfoDTO
    {
        public BadInfoDTO(string Status, string Reason)
        {
            this.Status = Status;
            this.Reason = Reason;
        }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("Reason")]
        public string Reason { get; set; }
    }
}