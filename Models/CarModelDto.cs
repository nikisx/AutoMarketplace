using AutoMarketplace.Data.Enum;

namespace AutoMarketplace.Models
{
    public class CarModelDto
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public string BodyType { get; set; }
        public string Engine { get; set; }
        public int FuelType { get; set; }
        public int StartYearOfProduction { get; set; }
        public int NumberOfDoors { get; set; }
        public double InTownFuelConsumptionPer100km { get; set; }
        public double OutOfTownFuelConsumptionPer100km { get; set; }
        public double CombinedFuelConsumptionPer100km { get; set; }
        public double TankVolume { get; set; }
        public string FuelString { get; set; }
        public double MaxKmForFullTankCombined { get; set; }
        public double MaxKmForFullTankOutOfTown { get; set; }
        public double MaxKmForFullTankInTown { get; set; }
    }
}
