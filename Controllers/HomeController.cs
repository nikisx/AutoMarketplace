using AutoMarketplace.Models;
using AutoMarketplace.Services.CarService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AutoMarketplace.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICarService carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            this.carService = carService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model =  this.carService.GetCarMakeList();
           
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string searchWord)
        {
            if (string.IsNullOrEmpty(searchWord))
            {
                var allMakesModel = this.carService.GetCarMakeList();

                return View(allMakesModel);
            }

            var model = carService.GetSerachedCarMakes(searchWord);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}