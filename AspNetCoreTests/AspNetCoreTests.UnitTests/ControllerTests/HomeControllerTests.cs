using AspNetCoreTests.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace AspNetCoreTests.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_public_view_for_anonymous_user()
        {
            var controller = new HomeController().WithAnonymousIdentity();

            var result = controller.Index() as ViewResult;

            Assert.Equal("PublicIndex", result.ViewName);
        }

        [Fact]
        public void Index_should_return_private_view_for_authenticated_user()
        {
            var controller = new HomeController()
                                    .WithIdentity("SomeValueHere", "gunnar@somecompany.com");

            var result = controller.Index() as ViewResult;
            var viewName = result.ViewName;
            
            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            var controller = new HomeController();

            var result = controller.Privacy() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Privacy");
        }
    }
}