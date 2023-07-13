using AutoMarketplace.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace AutoMarketplace.Models
{
    public class CarModelDto
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public string BodyType { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public int FuelType { get; set; }
        [Required]
        public int StartYearOfProduction { get; set; }
        [Required]
        public int NumberOfDoors { get; set; }
        [Required]
        public double InTownFuelConsumptionPer100km { get; set; }
        [Required]
        public double OutOfTownFuelConsumptionPer100km { get; set; }
        [Required]
        public double CombinedFuelConsumptionPer100km { get; set; }
        [Required]
        public double TankVolume { get; set; }
        public string FuelString { get; set; }
        public double MaxKmForFullTankCombined { get; set; }
        public double MaxKmForFullTankOutOfTown { get; set; }
        public double MaxKmForFullTankInTown { get; set; }
    }
}
