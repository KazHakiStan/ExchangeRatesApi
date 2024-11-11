using CurrencyConverterApi.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterApi.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly ExchangeRateService _exchangeRateService;
        
        public ExchangeRateController(ExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet(Name = "GetExchangeRates")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExchangeRatesResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var rates = await _exchangeRateService.GetExchangeRatesAsync();
                return Ok(rates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching exchange rates.", details = ex.Message });
            }
        }
    }
}
