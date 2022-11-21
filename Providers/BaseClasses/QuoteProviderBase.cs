using QuotesAPI.Models;
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
            //Get Rate from Server
            string quotesStr;
            using (var client = new HttpClient())
            {
                var url = string.Concat(_url, fromCurrencyCode);
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
                quotesStr = response.Content.ReadAsStringAsync().Result;
            }

            // Parse requested rate
            var rateStr = ParseQuote(quotesStr, toCurrencyCode.ToUpper());            

            // Wrap response
            if (rateStr == null || !decimal.TryParse(rateStr, out var rate))
                return null;
            return new Quote()
            {
                ExchangeRate = rate,
                FromCurrencyCode = fromCurrencyCode,
                ToCurrencyCode = toCurrencyCode,
                ProviderName = _providerName
            };
        }
    }
}
