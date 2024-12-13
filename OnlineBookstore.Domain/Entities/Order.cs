// OnlineBookstore.Domain/Entities/Order.cs
namespace OnlineBookstore.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;
    public int ShippingAddressId { get; set; }

    // Navigation properties
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}

// OnlineBookstore.Domain/Entities/OrderItem.cs
public class OrderItem
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTime { get; set; }

    // Navigation properties
    public Order Order { get; set; } = null!;
    public Book Book { get; set; } = null!;
}