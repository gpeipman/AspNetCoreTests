using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTests.UnitTests.ControllerTests
{
    public static class ControllerTestExtensions
    {
        public static T WithIdentity<T>(this T controller, string nameIdentifier, string name) where T: Controller
        {
            controller.EnsureHttpContext();

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                            {
                                    new Claim(ClaimTypes.NameIdentifier, nameIdentifier),
                                    new Claim(ClaimTypes.Name, name)
                                    // other required and custom claims
                            }, "TestAuthentication"));

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }

        public static T WithAnonymousIdentity<T>(this T controller) where T : Controller
        {
            controller.EnsureHttpContext();

            var principal = new ClaimsPrincipal(new ClaimsIdentity());

            controller.ControllerContext.HttpContext.User = principal;

            return controller;
        }

        private static T EnsureHttpContext<T>(this T controller) where T : Controller
        {
            if (controller.ControllerContext == null)
            {
                controller.ControllerContext = new ControllerContext();
            }

            if (controller.ControllerContext.HttpContext == null)
            {
                controller.ControllerContext.HttpContext = new DefaultHttpContext();
            }

            return controller;
        }
    }
}
