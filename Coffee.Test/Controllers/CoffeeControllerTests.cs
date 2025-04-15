using Coffee.API.Common;
using Coffee.API.Controllers;
using Coffee.API.Models;
using Coffee.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Coffee.Test
{
    public class CoffeeControllerTests
    {
        private readonly Mock<ICoffeeService> _coffeeService;
        private readonly CoffeeController _controller;

        public CoffeeControllerTests()
        {
            _coffeeService = new Mock<ICoffeeService>();          
            _controller = new CoffeeController(_coffeeService.Object);
        }


        [Fact]
        public async Task GetCoffee_ShouldReturnStatusCode200_WhenCalled()
        {
            // Arrange
            var defaultMessage = "Your piping hot coffee is ready";
            var response = new ResponseService<CoffeeDetail>
            {
                Data = new CoffeeDetail
                {
                    Message = defaultMessage,
                    Prepared = DateTime.Today.ToString("O")
                },
                StatusCode = Constants.StatusCodes.Success
            };

            _coffeeService
                .Setup(service => service.GetCoffee(It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.GetCoffee(CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var coffeeDetail = Assert.IsType<CoffeeDetail>(okResult.Value);
            Assert.Equal(defaultMessage, coffeeDetail.Message);
        }

    }
}