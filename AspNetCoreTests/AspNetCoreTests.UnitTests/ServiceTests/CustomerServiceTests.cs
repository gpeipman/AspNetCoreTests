using System.Threading.Tasks;
using AspNetCoreTests.Web.Data;
using AspNetCoreTests.Web.Services;
using Xunit;

namespace AspNetCoreTests.UnitTests.ServiceTests
{
    public class CustomerServiceTests : TestBase
    {
        [Fact]
        public async Task List_should_return_list_of_customers()
        {
            // Arrange
            using var dbContext = GetDbContext();
            dbContext.Customers.Add(new Customer { Id = 1, Name = "Company 1" });
            dbContext.Customers.Add(new Customer { Id = 2, Name = "Company 2" });
            await dbContext.SaveChangesAsync();

            // Act
            var service = new CustomerService(dbContext);
            var result = await service.List();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetCustomer_should_return_null_for_missing_customer()
        {
            using var dbContext = GetDbContext();
            var id = -1;

            var service = new CustomerService(dbContext);
            var result = await service.GetCustomer(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetCustomer_should_return_model_for_existing_customer()
        {
            var id = 1;
            using var dbContext = GetDbContext();
            dbContext.Customers.Add(new Customer { Id = 1, Name = "Company 1" });
            dbContext.Customers.Add(new Customer { Id = 2, Name = "Company 2" });
            await dbContext.SaveChangesAsync();

            var service = new CustomerService(dbContext);
            var result = await service.GetCustomer(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}