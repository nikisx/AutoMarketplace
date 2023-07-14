using AutoMarketplace.Data.Entities;
using AutoMarketplace.Models;

namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateMake(string name, IFormFile file, string userId);
        bool EditMake(CarMakeModel model, string userId);
        bool EditModel(CarModelDto model, string userId);
        bool AddModel(CarModelDto model, string userId);
        bool DeleteModel(int id);
        bool DeleteMake(int id);

        List<CarMakeModel> GetCarMakeList();
        List<CarMakeModel> GetSerachedCarMakes(string searchWord);

        CarMakeModel GetCarMakeById(int id, int pageNumber = 1);
        CarModelDto GetCarModelById(int id);
    }
}
