using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.Core.WebApp.IntegrationTests.Pages
{
    public class CommonPagesTests :
      IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CommonPagesTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/index")]
        [InlineData("/privacy")]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html",
                response.Content.Headers.ContentType.MediaType);
        }

        [Theory]
        [InlineData("/invalid-url")]
        public async Task EnsureInvalidUrlsRedirectTo404(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
