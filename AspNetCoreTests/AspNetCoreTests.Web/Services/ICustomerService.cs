using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTests.Web.Models;

namespace AspNetCoreTests.Web.Services
{
    public interface ICustomerService
    {
        Task<IList<CustomerModel>> List();
        Task<CustomerModel> GetCustomer(int id);
        Task SaveCustomer(CustomerModel customer);
    }
}