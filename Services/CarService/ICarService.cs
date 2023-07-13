using AutoMarketplace.Data.Entities;
using AutoMarketplace.Models;

namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateMake(string name, IFormFile file, string userId);
        bool AddModel(CarModelDto model, string userId);
        bool DeleteModel(int id);
        bool DeleteMake(int id);

        List<CarMakeModel> GetCarMakeList();

        CarMakeModel GetCarMakeById(int id);
        CarModelDto GetCarModelById(int id);
    }
}
