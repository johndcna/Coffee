using Coffee.API.Common;
using Coffee.API.Models;

namespace Coffee.API.Services.Interfaces
{
    public interface ICoffeeMessage
    {
        CoffeeResponse GetMessage(DateTime date);
    }

    //public class DefaultCoffeeMessageProvider : ICoffeeMessage
    //{
    //    public CoffeeResponse GetMessage(DateTime date)
    //    {
    //        return new CoffeeResponse
    //        {
    //           Message =  "Your piping hot coffee is ready"

    //        };
                
    //    }
    //}

    //public class AprilCoffeeMessageProvider : ICoffeeMessage
    //{
    //    public CoffeeResponse GetMessage(DateTime date)
    //    {
    //        if (date.Month == 4 && date.Day == 15)
    //        {
    //            return new CoffeeResponse
    //            {
    //                StatusCode = Constants.StatusCodes.ImATeaPot

    //            };
    //        }
            
    //        return null;
    //    }
    //}
}
