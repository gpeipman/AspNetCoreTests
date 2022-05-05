using System.Threading.Tasks;
using AspNetCoreTests.Web.Models;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View(nameof(Edit));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerModel model)
        {
            return await SaveCustomer(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerModel model)
        {
            return await SaveCustomer(model);
        }

        [NonAction]
        private async Task<IActionResult> SaveCustomer(CustomerModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if(!ModelState.IsValid)
            {
                return View(nameof(Edit), model);
            }

            await _customerService.SaveCustomer(model);

            return RedirectToAction(nameof(CustomersController.Index));
        }
    }
}