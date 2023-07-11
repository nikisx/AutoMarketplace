namespace AutoMarketplace.Services.CarService
{
    public interface ICarService
    {
        bool CreateMake(string name, IFormFile file);
    }
}
