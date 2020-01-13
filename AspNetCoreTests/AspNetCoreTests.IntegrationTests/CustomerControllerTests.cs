using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreTests.IntegrationTests
{
    public class CustomerControllerTests : TestBase
    {
        public CustomerControllerTests(TestApplicationFactory<FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Details")]
        [InlineData("/Customers/Details/1")]
        public async Task Get_EndpointsReturnFailToAnonymousUserForRestrictedUrls(string url)
        {
            // Arrange
            var client = Factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Details/1")]
        public async Task Get_EndPointsReturnsSuccessForRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Customers/Details")]
        public async Task Get_DetailsWithNoIdReturnsBadSuccessForRegularUser(string url)
        {
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            var response = await client.GetAsync(url);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}