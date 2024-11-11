using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ExchangeRateService
{
    private readonly HttpClient _httpClient;

    public ExchangeRateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> GetExchangeRatesAsync(string table = "A")
    {
        var url = $"https://api.nbp.pl/api/exchangerates/tables/{table}?format=json";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<object>(content);
    }
}
