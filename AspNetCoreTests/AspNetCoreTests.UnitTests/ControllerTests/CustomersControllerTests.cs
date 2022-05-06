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
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _controller = new CustomersController(_customerServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_index_or_default_view()
        {
            // Arrange
            var viewNames = new[] { null, "Index" };

            _customerServiceMock.Setup(c => c.List())
                                .ReturnsAsync(() => new List<CustomerModel>());

            // Act
            var result = await _controller.Index() as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.Contains(viewName, viewNames);
        }

        [Fact]
        public async Task Details_should_return_bad_request_for_missing_id()
        {
            // Arrange
            var id = (int?)null;

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_for_missing_customer()
        {
            // Arrange
            _customerServiceMock.Setup(c => c.GetCustomer(It.IsAny<int>()))
                               .ReturnsAsync(() => null);
            var id = -1;

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_should_return_details_view_for_existing_customer()
        {
            // Arrange
            var viewNames = new[] { null, "Details" };
            var model = new CustomerModel();
            var id = 10;
            _customerServiceMock.Setup(c => c.GetCustomer(It.IsAny<int>()))
                               .ReturnsAsync(() => model);
            
            // Act
            var result = await _controller.Details(id) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.Contains(viewName, viewNames);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public void Create_should_return_edit_view()
        {
            // Arrange
            var viewNames = new[] { null, "Edit" };

            // Act
            var result = _controller.Create() as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.Contains(viewName, viewNames);
        }

        [Fact]
        public async Task Create_should_return_bad_request_for_null_model()
        {
            // Arrange
            var model = (CustomerModel)null;

            // Act
            var result = await _controller.Create(model) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Create_should_return_to_index_after_successful_save()
        {
            // Arrange
            var model = new CustomerModel();
            _customerServiceMock.Setup(c => c.SaveCustomer(model));

            // Act
            var result = await _controller.Create(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_edit_view_when_customer_was_found()
        {
            // Arrange
            var viewNames = new[] { null, "Edit" };
            var id = 1;
            var model = new CustomerModel { Id = id };
            _customerServiceMock.Setup(service => service.GetCustomer(id))
                                .ReturnsAsync(model);

            // Act
            var result = await _controller.Edit(id) as ViewResult;
            var viewName = result.ViewName;

            // Assert
            Assert.Contains(viewName, viewNames);
            Assert.Equal(model, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_not_found_when_customer_was_not_found()
        {
            // Arrange
            var id = 1;
            var model = (CustomerModel)null;
            _customerServiceMock.Setup(service => service.GetCustomer(id))
                                .ReturnsAsync(model);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_bad_request_when_id_is_missing()
        {
            // Arrange
            var id = (int?)null;

            // Act
            var result = await _controller.Edit(id) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_bad_request_for_null_model()
        {
            // Arrange
            var model = (CustomerModel)null;

            // Act
            var result = await _controller.Edit(model) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_to_index_after_successful_save()
        {
            // Arrange
            var model = new CustomerModel();
            _customerServiceMock.Setup(c => c.SaveCustomer(model));

            // Act
            var result = await _controller.Create(model) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
