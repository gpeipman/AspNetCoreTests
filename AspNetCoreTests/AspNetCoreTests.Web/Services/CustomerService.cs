using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTests.Web.Data;
using AspNetCoreTests.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTests.Web.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DemoDbContext _dataContext;

        public CustomerService(DemoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CustomerModel> GetCustomer(int id)
        {
            var customer = await _dataContext.Customers.FindAsync(id);
            if(customer == null)
            {
                return null;
            }

            var model = new CustomerModel();
            model.Id = customer.Id;
            model.Address = customer.Address;
            model.Email = customer.Email;
            model.Name = customer.Name;

            return model;
        }

        public async Task<IList<CustomerModel>> List()
        {
            return await _dataContext.Customers
                                     .OrderBy(c => c.Name)
                                     .Select(c => new CustomerModel 
                                     { 
                                        Id = c.Id,
                                        Name = c.Name,
                                        Address = c.Address,
                                        Email = c.Email
                                     })
                                     .ToListAsync();
        }
    }
}