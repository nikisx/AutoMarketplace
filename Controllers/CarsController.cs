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
            if (string.IsNullOrWhiteSpace(name) || logo == null)
            {
               
                return RedirectToAction("Index", "Home", new {showError = true});
            }

            this.carService.CreateMake(name, logo, User.GetId());
                
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult EditMake(CarMakeModel model)
        {
            this.carService.EditMake(model, User.GetId());

            return RedirectToAction("CarMakeDetails", new { Id = model.Id });
        }

        [HttpPost]
        public IActionResult EditModel(CarModelDto model)
        {
            this.carService.EditModel(model, User.GetId());

            return RedirectToAction("CarModelDetails", new { Id = model.Id });
        }

        [HttpPost]
        public IActionResult AddModel(CarModelDto model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CarMakeDetails", new { Id = model.MakeId, showError = true });
            }

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
        public IActionResult CarMakeDetails(int Id, int pageNumber = 1)
        {
            var model  = this.carService.GetCarMakeById(Id, pageNumber);

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
