using Coffee.API.Common;
using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Providers
{
    public class WeatherCheckCoffeeMessageProvider : ICoffeeMessage
    {
        private readonly IWeatherService _weatherService;

        public WeatherCheckCoffeeMessageProvider(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task<CoffeeResponse> GetMessage(DateTime date)
        {
            var temperature = await _weatherService.GetCurrentTemperature(CancellationToken.None);
            if (temperature > 30)
            {
                return new CoffeeResponse
                {
                    Message = "Your refreshing iced coffee is ready",
                    StatusCode = Constants.StatusCodes.Success
                };
            }

            return null;
        }
    }
}
