using DemoExchangeRate.Interfaces.Services;
using DemoExchangeRate.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace DemoExchangeRate.Controllers
{
    [ApiController]
    [Route("api")]
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> _logger;
        private readonly IRateService _rateService;

        public RateController(ILogger<RateController> logger, IRateService rateService)
        {
            _logger = logger;
            _rateService = rateService;
        }

        [HttpGet    ]
        [Route("exchange-rates")]
        public async Task<IActionResult> GetExchagneRates([FromQuery] string country)
        {
            // get currency
            string? currency = Utility.Utility.GetCurrency(country);

            // currency not found, return NotFound
            if (currency == null)
            {
                return NotFound();
            }
            // get rates from service
            var rates = await _rateService.GetPangeaExchngeRatesAsync(country);
            return Ok(rates);
        }
    }
}
