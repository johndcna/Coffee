using Coffee.API.Common;
using Coffee.API.Common.Interfaces;
using Coffee.API.Models;
using Coffee.API.Services.Interfaces;

namespace Coffee.API.Services.Implementation
{
    public class CoffeeService : ICoffeeService
    {
        private readonly IEnumerable<ICoffeeMessage> _coffeeMessage;
        private readonly IDateProvider _dateProvider;

        public CoffeeService(IEnumerable<ICoffeeMessage> coffeeMessage, IDateProvider dateProvider)
        {
            _coffeeMessage = coffeeMessage;
            _dateProvider = dateProvider;
        }
        public async Task<ResponseService<CoffeeDetail>> GetCoffee(CancellationToken cancellationToken)
        {
            var today = _dateProvider.Today;

            var result = await _coffeeMessage
                       .Select(p => p.GetMessage(today))
                       .FirstOrDefault(m => m != null);

            return new ResponseService<CoffeeDetail>
            {
                Data = new CoffeeDetail
                {
                    Message = result?.Message,
                    Prepared = DateTime.Today.ToString("O")
                },
                StatusCode = result?.StatusCode ?? Constants.StatusCodes.NotFound
            };
        }
    }
}
