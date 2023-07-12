using AutoMarketplace.Data.Enum;

namespace AutoMarketplace.Data.Entities
{
    public class CarModel : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string BodyType { get; set; }
        public string Engine { get; set; }
        public FuelType FuelType { get; set; }
        public int StartYearOfProduction { get; set; }
        public int NumberOfDoors { get; set; }
        public int InTownFuelConsumptionPer100km { get; set; }
        public int OutOfTownFuelConsumptionPer100km { get; set; }
        public int CombinedFuelConsumptionPer100km { get; set; }

        public int MakeId { get; set; }
        public virtual CarMake Make { get; set; }

    }
}
