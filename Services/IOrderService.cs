namespace MyShop.Services;

public interface IOrderService
{
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int orderId);
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
}

