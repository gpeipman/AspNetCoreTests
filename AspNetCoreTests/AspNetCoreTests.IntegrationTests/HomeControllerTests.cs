using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTests.IntegrationTests
{
    public class HomeControllerTests : TestBase
    {
        public HomeControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/MissingPage")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}