using CurrencyConverterApi.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverterApi.Server.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;
    }
}
