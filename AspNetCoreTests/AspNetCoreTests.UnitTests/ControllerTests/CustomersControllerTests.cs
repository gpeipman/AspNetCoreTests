using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTests.Web.Controllers;
using AspNetCoreTests.Web.Models;
using AspNetCoreTests.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AspNetCoreTests.UnitTests.ControllerTests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_or_default_view()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(c => c.List())
                               .ReturnsAsync(() => new List<CustomerModel>());
            var controller = new CustomersController(customerServiceMock.Object);

            var result = await controller.Index() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact]
        public async Task Details_should_return_bad_request_for_missing_id()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var controller = new CustomersController(customerServiceMock.Object);
            var id = (int?)null;

            var result = await controller.Details(id);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_for_missing_customer()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(c => c.GetCustomer(It.IsAny<int>()))
                               .ReturnsAsync(() => null);
            var controller = new CustomersController(customerServiceMock.Object);
            var id = -1;

            var result = await controller.Details(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_should_return_details_view_for_existing_customer()
        {
            var model = new CustomerModel();
            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(c => c.GetCustomer(It.IsAny<int>()))
                               .ReturnsAsync(() => model);
            var controller = new CustomersController(customerServiceMock.Object);
            var id = 10;

            var result = await controller.Details(id) as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Details");
            Assert.Equal(model, result.Model);
        }
    }
}
