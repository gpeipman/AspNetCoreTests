using System.Threading.Tasks;
using AspNetCoreTests.Web.Data;
using AspNetCoreTests.Web.Models;

namespace AspNetCoreTests.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DemoDbContext _dataContext;

        public CustomerService(DemoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CustomerEditModel> GetCustomer(int id)
        {
            var customer = await _dataContext.Customers.FindAsync(id);
            if(customer == null)
            {
                return null;
            }

            var model = new CustomerEditModel();
            model.Id = customer.Id;
            model.Address = customer.Address;
            model.Email = customer.Email;
            model.Name = customer.Name;

            return model;
        }
    }
}