using Coffee.API.Common;
using Coffee.API.Models;

namespace Coffee.API.Services.Interfaces
{
    public interface ICoffeeMessage
    {
        Task<CoffeeResponse> GetMessage(DateTime date);
    }

}
