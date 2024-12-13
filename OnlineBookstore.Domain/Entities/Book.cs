namespace OnlineBookstore.Domain.Entities;

public class Book
{
    public int BookId { get; set; }
    public string ISBN { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime PublicationDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    // Navigation properties
    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public Inventory? Inventory { get; set; }
}
