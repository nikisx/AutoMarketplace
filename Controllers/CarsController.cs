using AutoMarketplace.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace AutoMarketplace.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarService carService;

        public CarsController(ICarService carService)
        {
            this.carService = carService;
        }

        [HttpPost]
        public IActionResult AddMake(string name, IFormFile logo)
        {
            this.carService.CreateMake(name, logo);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CarMakeDetails(int Id)
        {
            var model  = this.carService.GetCarMakeById(Id);

            return View(model);
        }
    }
}
