using Coffee.API.Common;
using Coffee.API.Common.Interfaces;
using Coffee.API.Models;
using Coffee.API.Services.Implementation;
using Coffee.API.Services.Interfaces;
using Moq;

namespace Coffee.Tests.Services
{
    public class CoffeeServiceTests
    {
        private readonly Mock<ICoffeeMessage> _coffeeDefaultProvider;
        private readonly Mock<ICoffeeMessage> _coffeeAprilProvider;
        private readonly Mock<IDateProvider> _dateProvider;
        public CoffeeServiceTests()
        {
            _coffeeDefaultProvider = new Mock<ICoffeeMessage>();

            _coffeeAprilProvider = new Mock<ICoffeeMessage>();

            _dateProvider = new Mock<IDateProvider>();
        }

        [Fact]
        public async Task GetCoffee_ShouldReturnStatusCode200_WhenCalledWithCurrentDate()
        {
            // Arrange
            var today = DateTime.Today;
            var expectedMessage = "Your piping hot coffee is ready";
            var expectedStatusCode = Constants.StatusCodes.Success;

            _coffeeDefaultProvider
                .Setup(p => p.GetMessage(today))
                .Returns((CoffeeResponse)null);

            _coffeeAprilProvider
                .Setup(p => p.GetMessage(today))
                .Returns(new CoffeeResponse
                {
                    Message = expectedMessage,
                    StatusCode = expectedStatusCode
                });

            _dateProvider.Setup(p => p.Today).Returns(DateTime.Today);

            var coffeeProviders = new List<ICoffeeMessage>
            {
                _coffeeDefaultProvider.Object,
                _coffeeAprilProvider.Object
            };

            var service = new CoffeeService(coffeeProviders, _dateProvider.Object);

            // Act
            var result = await service.GetCoffee(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(expectedMessage, result.Data.Message);
            Assert.Equal(today.ToString("O"), result.Data.Prepared);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Fact]
        public async Task GetCoffee_ShouldReturnStatusCode418_WhenCalledWithAprilFirst()
        {
            // Arrange
            var aprilFoolsDate = new DateTime(DateTime.Today.Year, 4, 1);

            _coffeeAprilProvider
                .Setup(p => p.GetMessage(aprilFoolsDate))
                .Returns(new CoffeeResponse
                {
                    StatusCode = Constants.StatusCodes.ImATeaPot,
                    Message = null
                });

            _coffeeDefaultProvider
                .Setup(p => p.GetMessage(aprilFoolsDate))
                .Returns((CoffeeResponse)null); 


            _dateProvider.Setup(p => p.Today).Returns(aprilFoolsDate);

            var coffeeProviders = new List<ICoffeeMessage>
            {
                _coffeeDefaultProvider.Object,
                _coffeeAprilProvider.Object
            };

            var service = new CoffeeService(coffeeProviders, _dateProvider.Object);

            // Act
            var result = await service.GetCoffee(CancellationToken.None);

            // Assert
            Assert.Equal(Constants.StatusCodes.ImATeaPot, result.StatusCode);
            Assert.Null(result.Data.Message);
        }




    }
}
