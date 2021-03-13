using AspNetCoreTests.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreTests.IntegrationTests
{
    public abstract class TestBase : IClassFixture<TestApplicationFactory<Startup, FakeStartup>>
    {
        protected WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase(TestApplicationFactory<Startup, FakeStartup> factory)
        {
            Factory = factory;            
        }

        // Add you other helper methods here
    }
}