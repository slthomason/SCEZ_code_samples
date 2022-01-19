using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace aspnet_dotnet.Models
{
    public class Nft
    {
        [JsonPropertyName("order_id")]
        public string OrderID { get; set; }

        [JsonPropertyName("msg")]
        public string Message { get; set; }
    }
}
