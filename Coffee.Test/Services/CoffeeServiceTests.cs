using Coffee.API.Controllers;
using Coffee.API.Models;
using Coffee.API.Services.Implementation;
using Coffee.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Tests.Services
{
    public class CoffeeServiceTests
    {
        private readonly Mock<ICoffeeMessage> _coffeeDefaultProvider;
        private readonly Mock<ICoffeeMessage> _coffeeAprilProvider;

        public CoffeeServiceTests()
        {
            _coffeeDefaultProvider = new Mock<ICoffeeMessage>();

            _coffeeAprilProvider = new Mock<ICoffeeMessage>();

         var providers = new List<ICoffeeMessage>
        {
            _coffeeDefaultProvider.Object,
            _coffeeAprilProvider.Object
        };

        }




        [Fact]
        public async Task GetCoffee_ShouldReturn200_WhenCalledWithCurrentDate()
        {
            // Arrange
            var today = DateTime.Today;
            var expectedMessage = "Your piping hot coffee is ready";
            var expectedStatusCode = 200;

            // Mock first provider: returns null (simulating no special message)
            _coffeeDefaultProvider
                .Setup(p => p.GetMessage(today))
                .Returns((CoffeeResponse)null);

            // Mock second provider: returns a valid message
            _coffeeAprilProvider
                .Setup(p => p.GetMessage(today))
                .Returns(new CoffeeResponse
                {
                    Message = expectedMessage,
                    StatusCode = expectedStatusCode
                });

            var coffeeProviders = new List<ICoffeeMessage>
        {
            _coffeeDefaultProvider.Object,
            _coffeeAprilProvider.Object
        };

            var service = new CoffeeService(coffeeProviders);

            // Act
            var result = await service.GetCoffee(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedMessage, result.Data.Message);
            Assert.Equal(today.ToString("O"), result.Data.Prepared);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        //[Fact]
        //public async Task GetCoffee_ShouldReturn200_WhenDateIsAprilFirst()
        //{
        //    // Arrange
        //    var april = new DateTime(2025,4,1); 
        //    var expectedMessage = "Your piping hot coffee is ready";
        //    var expectedStatusCode = 200;

        //    // Mock first provider: returns null (simulating no special message)
        //    _coffeeDefaultProvider
        //        .Setup(p => p.GetMessage(april))
        //        .Returns((CoffeeResponse)null);

        //    // Mock second provider: returns a valid message
        //    _coffeeAprilProvider
        //        .Setup(p => p.GetMessage(april))
        //        .Returns(new CoffeeResponse
        //        {
        //            Message = expectedMessage,
        //            StatusCode = expectedStatusCode
        //        });

        //    var coffeeProviders = new List<ICoffeeMessage>
        //{
        //    _coffeeDefaultProvider.Object,
        //    _coffeeAprilProvider.Object
        //};

        //    var service = new CoffeeService(coffeeProviders);

        //    // Act
        //    var result = await service.GetCoffee(CancellationToken.None);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.NotNull(result.Data);
        //    //Assert.Equal(expectedMessage, result.Data.Message);
        //    //Assert.Equal(april.ToString("O"), result.Data.Prepared);
        //    //Assert.Equal(expectedStatusCode, result.StatusCode);
        //}

        [Fact]
        public async Task GetCoffee_AprilFoolsDay_ReturnsStatusCode413()
        {
            // Arrange
            var aprilFoolsDate = new DateTime(DateTime.Today.Year, 4, 1);

            var mockAprilProvider = new Mock<ICoffeeMessage>();
            mockAprilProvider
                .Setup(p => p.GetMessage(aprilFoolsDate))
                .Returns(new CoffeeResponse
                {
                    StatusCode = 413,
                    Message = null
                });

            var mockDefaultProvider = new Mock<ICoffeeMessage>();
            mockDefaultProvider
                .Setup(p => p.GetMessage(aprilFoolsDate))
                .Returns((CoffeeResponse)null); // Won't be called if April provider handles it

            var service = new CoffeeService(new List<ICoffeeMessage> {
            mockAprilProvider.Object,
            mockDefaultProvider.Object
        });

            // Act
            var result = await service.GetCoffee(CancellationToken.None);

            // Assert
            Assert.Equal(413, result.StatusCode);
            Assert.Null(result.Data.Message);
        }




    }
}
