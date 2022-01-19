using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class Transactions
    {
        [JsonPropertyName("txn_id")]
        public int TxnId { get; set; }
        [JsonPropertyName("transaction_type")]
        public string TransactionType { get; set; }
        [JsonPropertyName("service_type")]
        public string ServiceType { get; set; }
        [JsonPropertyName("txn_amount")]
        public long TxnAmount { get; set; }
        [JsonPropertyName("total")]
        public long Total { get; set; }
        [JsonPropertyName("service_fee")]
        public long ServiceFee { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("txn_date")]
        public string TxnDate { get; set; }
        [JsonPropertyName("txn_status")]
        public string TxnStatus { get; set; }
    }
}
