using QuotesAPI.Parsers;
using QuotesAPI.Providers.BaseClasses;

namespace QuotesAPI.Providers
{
    public class ExchangeRateApiQuoteProvider : QuoteProviderBase
    {
        protected override string _providerName => "exchangerate-api";
        protected override string _url => "https://api.exchangerate-api.com/v4/latest/";

        protected override string ParseQuote(string quotesStr, string currency) => DefaultJsonParser.ParseQuote(quotesStr, currency);
    }
}
