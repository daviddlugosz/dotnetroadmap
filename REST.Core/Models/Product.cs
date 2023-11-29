namespace REST.Core.Models
{
    public class Product : IId
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public float Price { get; set; }
    }
}
