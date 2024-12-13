// OnlineBookstore.Domain/Entities/Author.cs
namespace OnlineBookstore.Domain.Entities;

public class Author
{
    public int AuthorId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Biography { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
