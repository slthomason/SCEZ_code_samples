using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class NftOrder
    {

        [JsonPropertyName("oid")]
        public string Oid { get; set; }
        [JsonPropertyName("order_date")]
        public string OrderDate { get; set; }
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("token_name")]
        public string TokenName { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("NftOrderStatus")]
        public string nft_order_status { get; set; }
        [JsonPropertyName("Asset")]
        public string asset { get; set; }
        [JsonPropertyName("AssetImage")]
        public string asset_image { get; set; }
    }
}
