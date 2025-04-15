using Coffee.API.Common;

namespace Coffee.API.Models
{
    public class CoffeeResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; } = Constants.StatusCodes.Success;
    }
}
