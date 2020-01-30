using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.WebUI.IntegrationTests.Controllers.Home
{
    public class HomePageTests :
      IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public HomePageTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/home")]
        [InlineData("/home/index")]
        [InlineData("/home/privacy")]
        [InlineData("/community/meta/home/index")]
        [InlineData("/community/meta/")]
        [InlineData("/community/meta/home")]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
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
