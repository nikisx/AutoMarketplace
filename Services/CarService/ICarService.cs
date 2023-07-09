namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateModel(string name, IFormFile file);
    }
}
