using Microsoft.EntityFrameworkCore;
using MyShop.Data;

namespace MyShop.Services;

public class OrderItemService : IOrderItemService
{
    private readonly ApplicationDbContext _context;

    public OrderItemService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .FirstOrDefaultAsync(oi => oi.Id == orderItemId);
    }

    public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
    {
        return await _context.OrderItems
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .ToListAsync();
    }

    public async Task CreateOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(int orderItemId)
    {
        var orderItem = await _context.OrderItems.FindAsync(orderItemId);
        if (orderItem != null)
        {
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        return await _context.OrderItems
            .Where(oi => oi.OrderId == orderId)
            .Include(oi => oi.Product)
            .Include(oi => oi.Order)
            .ToListAsync();
    }
}

