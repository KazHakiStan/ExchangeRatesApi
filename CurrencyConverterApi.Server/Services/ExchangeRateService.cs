using CurrencyConverterApi.Server.Data;
using CurrencyConverterApi.Server.Models;
using CurrencyConverterApi.Server.Schemas;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;
    private readonly Context _context;

    public ExchangeRateService(HttpClient httpClient, Context context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task<object?> GetExchangeRatesAsync(string table = "A")
    {
        var endpoint = $"https://api.nbp.pl/api/exchangerates/tables/{table}?format=json";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(content);
    }

    public async Task<object?> GetExchangeRatesAsync(string table, string? code = null)
    {
        
        var endpoint = code == null
            ? $"https://api.nbp.pl/api/exchangerates/tables/{table}?format=json"
            : $"https://api.nbp.pl/api/exchangerates/rates/{table}/{code}?format=json";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(content);
    }

    public async Task<object?> GetRatesByDateAsync(string table, DateTime dateTime)
    {
        string dateString = dateTime.ToString("yyyy-MM-dd");
        var endpoint = $"https://api.nbp.pl/api/exchangerates/tables/{table}/{dateString}?format=json";
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(content);
    }

    public async Task SaveExchangeRatesAsync(IEnumerable<ExchangeRate> rates)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _context.ExchangeRates.RemoveRange(_context.ExchangeRates);
            await _context.SaveChangesAsync();
            
            await _context.ExchangeRates.AddRangeAsync(rates);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<ExchangeRate>> GetExchangeRatesAsync()
    {
        return await _context.ExchangeRates.ToListAsync();
    }
}
