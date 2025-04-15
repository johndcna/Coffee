using Coffee.API.Models;

namespace Coffee.API.Services.Interfaces
{
    public interface ICoffeeService
    {
        Task<ResponseService<CoffeeDetail>> GetCoffee(CancellationToken cancellationToken);
    }
}
