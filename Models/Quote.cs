using Newtonsoft.Json;
using System;

namespace QuotesAPI.Models
{
    public class Quote
    {
        [JsonProperty("exchange_rate")]
        public decimal ExchangeRate { get; set; }

        [JsonIgnore]
        public string FromCurrencyCode { get; set; }

        [JsonProperty("currency_code")]
        public string ToCurrencyCode { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; } // Filled later by caller

        /// <summary>
        /// Returns the best quote of a pair of quotes
        public static Quote Compare(Quote quote1, Quote quote2)
        {
            // Handle no quote
            if (quote1 == null)
                return quote2;
            if (quote2 == null)
                return quote1;
            // Handle equal quote
            if (quote1.ExchangeRate == quote2.ExchangeRate)
                return new Random().Next(2) == 0 ? quote1 : quote2;
            // Compare quotes
            return quote1.ExchangeRate < quote2.ExchangeRate ? quote1 : quote2;
        }
    }
}