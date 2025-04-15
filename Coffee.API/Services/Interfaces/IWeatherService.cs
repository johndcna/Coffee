namespace Coffee.API.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperatureAsync(CancellationToken cancellationToken);
    }
}
