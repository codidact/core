using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Codidact.Core.WebApp.IntegrationTests.Pages
{
    public class QuestionsPageTests :
      IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public QuestionsPageTests(
            CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("main/questions")]
        [InlineData("main/questions?skip=0&take=20")]
        public async Task ReturnsQuestionsByQueryParams(string url)
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
        [InlineData("wrong-category/questions")]
        public async Task IncorrectCategoryThrowsServerError(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
