using Coffee.API.Common;
using Coffee.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Coffee.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeeService;
        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        [HttpGet("/brew-coffee")]
        [EnableRateLimiting("LimitOnFifthCall")]
        public async Task<ActionResult> GetCoffee(CancellationToken cancellationToken)
        {
            var result = await _coffeeService.GetCoffee(cancellationToken);

            if (result.StatusCode != Constants.StatusCodes.Success) 
            {
                return StatusCode((int)result.StatusCode,null);
            }

            return Ok(result.Data);
        }
    }
    }
