using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Providers
{
    public class DefaultCoffeeMessageProvider : ICoffeeMessage
    {
        public CoffeeResponse GetMessage(DateTime date)
        {
            return new CoffeeResponse
            {
                Message = "Your piping hot coffee is ready"

            };

        }
    }
}
