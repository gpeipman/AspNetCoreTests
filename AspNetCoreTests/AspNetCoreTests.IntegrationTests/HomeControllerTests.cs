using System;
using System.Security.Claims;
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
        [InlineData("/Home/Privacy")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = Factory.CreateClientWithTestAuth(new[] {
                new Claim(ClaimTypes.Name, "Gunnar"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}