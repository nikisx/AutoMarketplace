using Microsoft.AspNetCore.Mvc;

namespace AutoMarketplace.Controllers
{
    public class CarsController : Controller
    {
        [HttpPost]
        public IActionResult AddMake(string name, IFormFile logo)
        {


            return RedirectToAction("Index", "Home");
        }
    }
}
