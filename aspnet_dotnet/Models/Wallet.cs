using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace aspnet_dotnet.Models
{
    public class Wallet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("wallet_address")]
        public string WalletAddress { get; set; }

        [JsonPropertyName("wallet_name")]
        public string WalletName { get; set; }

        [JsonPropertyName("wallet_balance")]
        public int WalletBalance { get; set; }

    }
}
