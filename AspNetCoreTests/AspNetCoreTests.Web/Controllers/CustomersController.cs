using System.Threading.Tasks;
using AspNetCoreTests.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTests.Web.Controllers
{
    [Authorize]
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

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }

            var model = await _customerService.GetCustomer(id.Value);
            if(model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var model = await _customerService.GetCustomer(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}