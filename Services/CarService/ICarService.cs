using AutoMarketplace.Data.Entities;
using AutoMarketplace.Models;

namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateMake(string name, IFormFile file, string userId);
        bool AddModel(CarModelDto model, string userId);

        List<CarMakeModel> GetCarMakeList();

        CarMakeModel GetCarMakeById(int id);
    }
}
