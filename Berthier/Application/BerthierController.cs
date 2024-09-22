using Berthier.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YahooFinanceApi;

namespace Berthier.Application
{
    [Route("api/berthier")]
    [ApiController]
    public class BerthierController : ControllerBase
    {
        private readonly IBerthierService _berthierService;
        public BerthierController(IBerthierService berthierService)
        {
            _berthierService = berthierService;
        }

        [HttpGet(template: "spots", Name="GetSpots")]
        public async Task<IDictionary<string, IEnumerable<Candle>>> GetSpots(List<string> tickers, DateTime startDate, DateTime endDate, string interval)
        {
            return await _berthierService.GetSpots(tickers, startDate, endDate, interval).ConfigureAwait(false);
        }
    }
}
