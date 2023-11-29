namespace REST.Core.Models
{
    public class Customer : IId
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
}
