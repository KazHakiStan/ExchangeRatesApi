using System.ComponentModel.DataAnnotations;

namespace CurrencyConverterApi.Server.Schemas
{
    public class Rate
    {
        [Required]
        public string? Currency { get; set; }
        [Required]
        public string? Code { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be a positive value.")]
        public double Mid { get; set; }
    }

    public class ExchangeRatesResponse
    {
        [Required]
        public string? Table { get; set; }
        [Required]
        public string? No { get; set; }
        [Required]
        public string? EffectiveDate { get; set; }
        [Required]
        public List<Rate>? Rates { get; set; }
    }
}