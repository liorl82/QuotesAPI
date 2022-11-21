using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using QuotesAPI.HttpHandlers;

namespace QuotesAPI.Controllers
{
    public class QuoteController : ApiController
    {
        public async Task<string> Get(string from_currency_code, decimal amount, string to_currency_code)
        {
            var res = await QuoteHttpHandler.GetQuote(from_currency_code, to_currency_code, amount);
            return res != null ? 
                   JsonConvert.SerializeObject(res) :
                   $"Failed to retrieve quote for {from_currency_code} => {to_currency_code}";
        }        
    }
}
