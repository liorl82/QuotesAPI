using QuotesAPI.Models;
using System;
using System.Net.Http;

namespace QuotesAPI.Providers.BaseClasses
{
    public abstract class QuoteProviderBase
    {
        protected virtual string _providerName { get; }
        protected virtual string _url { get; }

        /// <summary>
        /// Extracts a conversion rate for a specific currency
        /// </summary>
        protected abstract string ParseQuote(string quotesStr, string currency);

        /// <summary>
        /// Gets a quote for a given pair of currencies
        /// </summary>
        public Quote GetQuote(string fromCurrencyCode, string toCurrencyCode)
        {
            Quote quote = null;
            //Get Rate from Server
            string quotesStr = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = null;
                var url = string.Concat(_url, fromCurrencyCode);
                try
                {
                    response = client.GetAsync(url).Result;
                }
                // Hush in case currency doesn't exist in provider
                catch (Exception) { } 

                if (response != null && response.IsSuccessStatusCode)
                    quotesStr = response.Content.ReadAsStringAsync().Result;
            }

            // Parse requested rate
            if (!string.IsNullOrEmpty(quotesStr))
            {
                var rateStr = ParseQuote(quotesStr, toCurrencyCode.ToUpper());
                // Wrap response
                if (rateStr != null && decimal.TryParse(rateStr, out var rate))
                {
                    quote = new Quote()
                    {
                        ExchangeRate = rate,
                        FromCurrencyCode = fromCurrencyCode,
                        ToCurrencyCode = toCurrencyCode,
                        ProviderName = _providerName
                    };
                }                
            }

            return quote;
        }
    }
}
