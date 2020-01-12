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
            var client = Factory.CreateClientWithTestAuth(TestClaimsProvider.WithAdminClaims());

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}