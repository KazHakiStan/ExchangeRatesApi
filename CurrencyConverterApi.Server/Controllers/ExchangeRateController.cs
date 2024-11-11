using CurrencyConverterApi.Server.Data;
using CurrencyConverterApi.Server.Models;
using CurrencyConverterApi.Server.Schemas;
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
                var rates = await _exchangeRateService.GetExchangeRatesAsync("A", null);
                return Ok(rates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching exchange rates.", details = ex.Message });
            }
        }

        [HttpGet("{table}")]
        public async Task<IActionResult> GetRates(string table, [FromQuery] string? code = null)
        {
            try
            {
                var rates = await _exchangeRateService.GetExchangeRatesAsync(table, code);
                return Ok(rates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }

        [HttpGet("{table}/{date}")]
        public async Task<IActionResult> GetRatesByDate(string table, DateTime date)
        {
            try
            {
                var rates = await _exchangeRateService.GetRatesByDateAsync(table, date);
                return Ok(rates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", details = ex.Message });
            }
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveExchangeRates([FromBody] List<ExchangeRate> rates)
        {
            try
            {
                await _exchangeRateService.SaveExchangeRatesAsync(rates);
                return Ok("Exchange rates saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while sving exchange rates.", details = ex.Message });
            }
        }
    }
}
