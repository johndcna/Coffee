using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Implementation
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OpenWeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<double> GetCurrentTemperature(CancellationToken cancellationToken)
        {
            var apiKey = _configuration["OpenWeatherMap:ApiKey"];
            var url = $"https://api.openweathermap.org/data/2.5/weather?q=London&appid={apiKey}";

            var response = await _httpClient.GetFromJsonAsync<OpenWeatherResponse>(url, cancellationToken);

            return response?.Main?.Temp ?? 0;
        }

        private class OpenWeatherResponse
        {
            public Weather Main { get; set; }
        }

        private class Weather
        {
            public double Temp { get; set; }
        }
    }
}
