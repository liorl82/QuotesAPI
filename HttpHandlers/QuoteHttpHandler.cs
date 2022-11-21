using QuotesAPI.Models;
using QuotesAPI.Providers;
using QuotesAPI.Providers.BaseClasses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace QuotesAPI.HttpHandlers
{
    public class QuoteHttpHandler
    {
        static List<QuoteProviderBase> _quoteProviders;

        static QuoteHttpHandler()
        {            
            _quoteProviders = new List<QuoteProviderBase>() { new ExchangeRateApiQuoteProvider(), new FrankfurterQuoteProvider() };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        /// <summary>
        /// Gets a quote for a given pair of currencies
        /// </summary>
        public static async Task<Quote> GetQuote(string fromCurrencyCode, string toCurrencyCode, decimal amount)
        {
            Quote bestQuote = null;
            foreach (var provider in _quoteProviders)
            {
                var currQuote = await provider.GetQuote(fromCurrencyCode, toCurrencyCode);
                bestQuote = Quote.Compare(bestQuote, currQuote);
            }
            if (bestQuote != null)
                bestQuote.Amount = Math.Round(amount * bestQuote.ExchangeRate);
            return bestQuote;
        }
    }
}