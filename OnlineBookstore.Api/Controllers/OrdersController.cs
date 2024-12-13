using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.Domain.Entities;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Domain.DTOs;
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderCreateDto orderDto)
    {
        var order = await _orderService.CreateOrderAsync(orderDto);
        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _orderService.GetOrderAsync(id);
        if (order == null)
            return NotFound();
        return Ok(order);
    }
}