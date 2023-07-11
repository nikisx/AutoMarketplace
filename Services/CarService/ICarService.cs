using AutoMarketplace.Data.Entities;
using AutoMarketplace.Models;

namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateMake(string name, IFormFile file);

        List<CarMakeModel> GetCarMakeList();

        CarMakeModel GetCarMakeById(int id);
    }
}
