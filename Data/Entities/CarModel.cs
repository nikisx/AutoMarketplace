namespace AutoMarketplace.Data.Entities
{
    public class CarModel : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int MakeId { get; set; }
        public virtual CarMake Make { get; set; }

    }
}
