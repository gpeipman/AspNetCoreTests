using AspNetCoreTests.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace AspNetCoreTests.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_index_view()
        {
            var logger = new NullLogger<HomeController>();
            var controller = new HomeController(logger);

            var result = controller.Index() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }
    }
}