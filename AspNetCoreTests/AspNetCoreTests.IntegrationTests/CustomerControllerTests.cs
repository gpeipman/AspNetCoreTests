using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AspNetCoreTests.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreTests.IntegrationTests
{
    [Collection("Sequential")]
    public class CustomerControllerTests : TestBase
    {
        public CustomerControllerTests(TestApplicationFactory<Startup,FakeStartup> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Details")]
        [InlineData("/Customers/Details/1")]
        [InlineData("/Customers/Edit")]
        [InlineData("/Customers/Edit/1")]
        public async Task Get_EndpointsReturnFailToAnonymousUserForRestrictedUrls(string url)
        {
            // Arrange
            var client = Factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

            // Act
            var response = await client.GetAsync(url);
            var redirectUrl = response.Headers.Location.LocalPath;

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);            
            Assert.Equal("/auth/login", redirectUrl);            
        }

        [Fact]
        public async Task Edit_EndpointReturnSuccessForCorrectModel()
        {
            // Arrange
            var claimsProvider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(claimsProvider);

            var formValues = new Dictionary<string, string>();
            formValues.Add("Id", "121");
            formValues.Add("Address", "Hobujaama 1");
            formValues.Add("Name", "John Smith");
            formValues.Add("Email", "john@example.com");

            var content = new FormUrlEncodedContent(formValues);

            // Act
            var response = await client.PostAsync("/Customers/Edit", content);

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Details/1")]
        public async Task Get_EndPointsReturnsSuccessForRegularUser(string url)
        {
            // Arrange
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/Customers/Edit/")]
        [InlineData("/Customers/Edit/1")]
        public async Task Get_EditReturnsFailToRegularUser(string url)
        {
            // Arrange
            var provider = TestClaimsProvider.WithUserClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Details/1")]
        [InlineData("/Customers/Edit/1")]
        public async Task Get_EndPointsReturnsSuccessForAdmin(string url)
        {
            // Arrange
            var provider = TestClaimsProvider.WithAdminClaims();
            var client = Factory.CreateClientWithTestAuth(provider);

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}