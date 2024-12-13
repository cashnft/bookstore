namespace OnlineBookstore.Domain.Entities;

public class Inventory
{
    public int BookId { get; set; }
    public int StockLevel { get; set; }
    public DateTime? LastRestocked { get; set; }
    
    // Navigation property
    public Book Book { get; set; } = null!;
}