using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        public IActionResult Index()
        {
            var customer = new Customer();
            return View(customer);
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
