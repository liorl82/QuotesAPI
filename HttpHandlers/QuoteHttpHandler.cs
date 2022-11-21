using QuotesAPI.Models;
using QuotesAPI.Providers;
using QuotesAPI.Providers.BaseClasses;
using System;
using System.Collections.Generic;
using System.Net;

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
        public static Quote GetQuote(string fromCurrencyCode, string toCurrencyCode, decimal amount)
        {
            Quote bestQuote = null;
            foreach (var provider in _quoteProviders)
            {
                var currQuote = provider.GetQuote(fromCurrencyCode, toCurrencyCode);
                bestQuote = Quote.Compare(bestQuote, currQuote);
            }
            bestQuote.Amount = Math.Round(amount * bestQuote.ExchangeRate);
            return bestQuote;
        }
    }
}