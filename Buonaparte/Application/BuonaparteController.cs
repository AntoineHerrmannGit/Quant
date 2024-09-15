using Microsoft.AspNetCore.Mvc;
using Buonaparte.Interface;
using Models.Models.Logger.Interface;

namespace Buonaparte.Application
{
    [Route("api/buonaparte")]
    [ApiController]
    public class BuonaparteController : ControllerBase
    {
        private ILogger _logger;
        private IBuonaparteService _buonaparteService;

        public BuonaparteController(ILogger logger, IBuonaparteService buonaparteService)
        {
            _logger = logger;
            _buonaparteService = buonaparteService;
        }
    }
}