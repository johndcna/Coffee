namespace Coffee.API.Services.Interfaces
{
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperature(CancellationToken cancellationToken);
    }
}
