using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Domain.DTOs;
using OnlineBookstore.Domain.Interfaces;  // For ICacheService
using OnlineBookstore.Infrastructure.Data;

namespace OnlineBookstore.Application.Services;

public class OrderService
{
    private readonly BookstoreContext _context;
    private readonly ICacheService _cache;

    public OrderService(BookstoreContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<Order> CreateOrderAsync(OrderCreateDto orderDto)
    {
        var order = new Order
        {
            CustomerId = orderDto.CustomerId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = orderDto.TotalAmount,
            Status = "Pending",
            ShippingAddressId = orderDto.ShippingAddressId
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }
}