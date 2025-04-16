using Coffee.API.Services.Implementation;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Coffee.Tests.Services
{
    public class OpenWeatherServiceTests
    {
        private HttpClient _httpClient;
        private readonly Mock<IConfiguration> _configuration;

        public OpenWeatherServiceTests()
        {
            _configuration = new Mock<IConfiguration>();

        }

        [Fact]
        public async Task GetCurrentTemperature_ReturnsCorrectTemperature()
        {
            // Arrange
            _configuration.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns("fake-api-key");


            var response = new
            {
                main = new { temp = 25.3 }
            };
            var json = JsonSerializer.Serialize(response);

            var mockHttpHandler = new Mock<HttpMessageHandler>();
            mockHttpHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                });

            // Create HttpClient using the mocked handler
            _httpClient = new HttpClient(mockHttpHandler.Object);

            var service = new OpenWeatherService(_httpClient, _configuration.Object);

            // Act
            var result = await service.GetCurrentTemperature(CancellationToken.None);

            // Assert
            Assert.Equal(25.3, result);
        }
    }
}
