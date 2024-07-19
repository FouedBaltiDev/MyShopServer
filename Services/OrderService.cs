using Microsoft.EntityFrameworkCore;
using MyShop.Data;

namespace MyShop.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Include(o => o.Delivery)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Include(o => o.Delivery)
            .ToListAsync();
    }

    public async Task CreateOrderAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.User)
            .Include(o => o.OrderItems)
            .Include(o => o.Delivery)
            .ToListAsync();
    }
}

