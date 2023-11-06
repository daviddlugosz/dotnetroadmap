namespace REST.Core.Models
{
    public class LibraryUser
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime DateJoined { get; set; }
        public List<int> BorrowedBooks { get; set; } = new List<int>();
    }
}
