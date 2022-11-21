using QuotesAPI.Parsers;
using QuotesAPI.Providers.BaseClasses;

namespace QuotesAPI.Providers
{
    public class FrankfurterQuoteProvider : QuoteProviderBase
    {
        protected override string _providerName => "Frankfurter";
        protected override string _url => "https://api.frankfurter.app/latest?from=";

        protected override string ParseQuote(string quotesStr, string currency) => DefaultJsonParser.ParseQuote(quotesStr, currency);
    }
}
