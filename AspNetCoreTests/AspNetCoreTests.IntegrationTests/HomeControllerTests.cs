using System.Threading.Tasks;
using AspNetCoreTests.Web;
using Xunit;

namespace AspNetCoreTests.IntegrationTests
{
    [Collection("Sequential")]
    public class HomeControllerTests : TestBase
    {
        public HomeControllerTests(TestApplicationFactory<Startup, FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            using var client = Factory.CreateClientWithTestAuth(claimsProvider);
            
            // Act
            using var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Home/Privacy")]
        public async Task Get_AnonymousCanAccessPrivacy(string url)
        {
            // Arrange
            using var client = Factory.CreateClient();

            // Act
            using var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}