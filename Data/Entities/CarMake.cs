namespace AutoMarketplace.Data.Entities
{
    public class CarMake : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public virtual ICollection<CarModel> Models { get; set; } = new HashSet<CarModel>();
    }
}
