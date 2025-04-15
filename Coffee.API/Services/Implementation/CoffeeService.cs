using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Implementation
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IEnumerable<ICoffeeMessage> _coffeeMessage;

        public CoffeeService(IEnumerable<ICoffeeMessage> coffeeMessage)
        {
            _coffeeMessage = coffeeMessage;
        }
        public async Task<ResponseService<CoffeeDetail>> GetCoffee(CancellationToken cancellationToken)
        {
            var today = DateTime.Today;

            var result = _coffeeMessage
                       .Select(p => p.GetMessage(today))
                       .FirstOrDefault(m => m != null);

            return new ResponseService<CoffeeDetail>
            {
                Data = new CoffeeDetail
                {
                    Message = result.Message,
                    Prepared = DateTime.Today.ToString("O")
                },
                StatusCode = result.StatusCode
            };
        }
    }
}
