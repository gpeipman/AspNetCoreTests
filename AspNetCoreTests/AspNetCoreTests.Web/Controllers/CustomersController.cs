using System.Threading.Tasks;
using AspNetCoreTests.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTests.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.List();

            return View(customers);
        }
    }
}