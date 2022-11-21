using Newtonsoft.Json.Linq;

namespace QuotesAPI.Parsers
{
    public static class DefaultJsonParser
    {
        public static string ParseQuote(string quotesStr, string currency)
        {
            var quotesJson = JObject.Parse(quotesStr);
            var rateStr = quotesJson.SelectToken($@"rates.{currency}")?.ToString();
            return rateStr;
        }
    }
}