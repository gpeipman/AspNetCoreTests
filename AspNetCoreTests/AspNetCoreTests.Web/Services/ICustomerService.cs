using System.Threading.Tasks;
using AspNetCoreTests.Web.Models;

namespace AspNetCoreTests.Web.Services
{
    public interface ICustomerService
    {
        Task<CustomerEditModel> GetCustomer(int id);
    }
}