namespace MyShop.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyShop.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OrderItemsController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemsController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
    {
        var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }
        return Ok(orderItem);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
    {
        var orderItems = await _orderItemService.GetAllOrderItemsAsync();
        return Ok(orderItems);
    }

    [HttpPost]
    public async Task<ActionResult> CreateOrderItem(OrderItem orderItem)
    {
        await _orderItemService.CreateOrderItemAsync(orderItem);
        return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.Id }, orderItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
    {
        if (id != orderItem.Id)
        {
            return BadRequest();
        }

        var existingOrderItem = await _orderItemService.GetOrderItemByIdAsync(id);
        if (existingOrderItem == null)
        {
            return NotFound();
        }

        await _orderItemService.UpdateOrderItemAsync(orderItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }

        await _orderItemService.DeleteOrderItemAsync(id);
        return NoContent();
    }

    [HttpGet("order/{orderId}")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId(int orderId)
    {
        var orderItems = await _orderItemService.GetOrderItemsByOrderIdAsync(orderId);
        return Ok(orderItems);
    }
}


