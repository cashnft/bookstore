// OnlineBookstore.Domain/DTOs/OrderCreateDto.cs
namespace OnlineBookstore.Domain.DTOs;

public class OrderCreateDto
{
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public int ShippingAddressId { get; set; }
}