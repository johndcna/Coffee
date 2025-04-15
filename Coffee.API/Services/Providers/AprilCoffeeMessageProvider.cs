using Coffee.API.Common;
using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Providers
{
    public class AprilCoffeeMessageProvider : ICoffeeMessage
    {
        public CoffeeResponse GetMessage(DateTime date)
        {
            if (date.Month == 4 && date.Day == 1)
            {
                return new CoffeeResponse
                {
                    StatusCode = Constants.StatusCodes.ImATeaPot

                };
            }

            return null;
        }
    }
}
