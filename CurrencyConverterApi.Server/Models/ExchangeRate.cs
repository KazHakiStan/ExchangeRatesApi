namespace CurrencyConverterApi.Server.Models
{
    public class ExchangeRate
    {
        public int Id {  get; set; } 
        public string Currency { get; set; } = null!;
        public string Code { get; set; } = null!;
        public double Mid { get; set; }
    }
}