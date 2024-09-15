using Microsoft.AspNetCore.Mvc;
using Models;
using Ney.Interface;
using Models.Models.Logger.Interface;

namespace Ney.Application
{
    [Route("api/ney")]
    [ApiController]
    public class NeyController : ControllerBase
    {
        private ILogger _logger;
        private INeyService _neyService;

        public NeyController(ILogger logger, INeyService neyService)
        {
            _logger = logger;
            _neyService = neyService;
        }

        [HttpGet("price/option")]
        public async Task<IActionResult> PriceOption(Option option, Spot spot, double volatility, double rate, DateTime calculationDate)
        {
            var price = await _neyService.PriceOption(option, spot, volatility, rate, calculationDate).ConfigureAwait(false);
            return Ok(price);
        }

        [HttpGet("price/greek")]
        public async Task<IActionResult> Greeks(Option option, Spot spot, double volatility, double rate, DateTime calculationDate)
        {
            var price = await _neyService.PriceOption(option, spot, volatility, rate, calculationDate).ConfigureAwait(false);
            return Ok(price);
        }
    }
}
