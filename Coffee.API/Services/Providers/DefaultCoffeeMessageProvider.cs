using Coffee.API.Common;
using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Providers
{
    public class DefaultCoffeeMessageProvider : ICoffeeMessage
    {
        public Task<CoffeeResponse> GetMessage(DateTime date)
        {
            return Task.FromResult(new CoffeeResponse
            {
                Message = "Your piping hot coffee is ready",
                StatusCode = Constants.StatusCodes.Success
            });

        }
    }
}
