using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class WalletBalance
    {
        [JsonPropertyName("credit")]
        public long Credit { get; set; }
        [JsonPropertyName("debit")]
        public long Debit { get; set; }
        [JsonPropertyName("balance")]
        public long Balance { get; set; }
        [JsonPropertyName("default_wallet_address")]
        public string DefaultWalletAddress { get; set; }
        [JsonPropertyName("default_wallet_id")]
        public int DefaultWalletId { get; set; }
        [JsonPropertyName("internal_wallet_address")]
        public string InternalWalletAddress { get; set; }
        [JsonPropertyName("is_phone_verified")]
        public bool IsPhoneVerified { get; set; }
        [JsonPropertyName("IsEmailVerified")]
        public bool is_email_verified { get; set; }
        [JsonPropertyName("Status")]
        public int status { get; set; }
        [JsonPropertyName("TotalNft")]
        public int total_nft { get; set; }
        [JsonPropertyName("NftSize")]
        public string nft_size { get; set; }
        [JsonPropertyName("TotalTxn")]
        public int total_txn { get; set; }
        [JsonPropertyName("DayTxnCount")]
        public int day_txn_count { get; set; }
    }
}
