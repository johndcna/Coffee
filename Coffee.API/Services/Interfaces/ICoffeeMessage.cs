using Coffee.API.Models;

namespace Coffee.API.Services.Interfaces
{
    public interface ICoffeeMessage
    {
        CoffeeResponse GetMessage(DateTime date);
    }

    public class DefaultCoffeeMessageProvider : ICoffeeMessage
    {
        public CoffeeResponse GetMessage(DateTime date)
        {
            return new CoffeeResponse
            {
               Message =  "Your piping hot coffee is ready"

            };
                
        }
    }

    public class AprilCoffeeMessageProvider : ICoffeeMessage
    {
        public CoffeeResponse GetMessage(DateTime date)
        {
            if (date.Month == 4 && date.Day == 1)
            {
                return new CoffeeResponse
                {
                    StatusCode = 413

                };
            }

            return null;
        }
    }
}
