namespace AutoMarketplace.Models
{
    public class CarMakeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public IFormFile File { get; set; }

        public ICollection<CarModelDto> Models { get; set; }
    }
}
