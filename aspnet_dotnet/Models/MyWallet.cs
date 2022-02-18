using System.Text.Json.Serialization;

namespace aspnet_dotnet.Models
{
    public class MyWallet
    {
        [JsonPropertyName("wallet_id")]
        public int WalletId { get; set; }
        [JsonPropertyName("wallet_address")]
        public string WalletAddress { get; set; }
        [JsonPropertyName("total_balance")]
        public long TotalBalance { get; set; }
        [JsonPropertyName("locked_balance")]
        public long LockedBalance { get; set; }
        [JsonPropertyName("transaction_data")]
        public string TransactionData { get; set; }
        [JsonPropertyName("is_passphrase")]
        public bool IsPassPhrase { get; set; }
    }
}
