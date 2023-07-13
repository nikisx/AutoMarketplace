using AutoMarketplace.Extensions;
using AutoMarketplace.Models;
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
            this.carService.CreateMake(name, logo, User.GetId());

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddModel(CarModelDto model)
        {
            this.carService.AddModel(model, User.GetId());

            return RedirectToAction("CarMakeDetails",new { Id = model.MakeId});
        }

        [HttpPost]
        public IActionResult DeleteModel(int id, int carMakeId)
        {
            this.carService.DeleteModel(id);

            return RedirectToAction("CarMakeDetails",new { Id = carMakeId });
        }

        [HttpPost]
        public IActionResult DeleteMake(int id)
        {
            this.carService.DeleteMake(id);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult CarMakeDetails(int Id)
        {
            var model  = this.carService.GetCarMakeById(Id);

            return View(model);
        }

        [HttpGet]
        public IActionResult CarModelDetails(int Id)
        {
            var model  = this.carService.GetCarModelById(Id);

            return View(model);
        }
    }
}
