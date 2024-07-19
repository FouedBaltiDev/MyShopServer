namespace MyShop.Services;

public interface IOrderItemService
{
    Task<OrderItem> GetOrderItemByIdAsync(int orderItemId);
    Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync();
    Task CreateOrderItemAsync(OrderItem orderItem);
    Task UpdateOrderItemAsync(OrderItem orderItem);
    Task DeleteOrderItemAsync(int orderItemId);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
}
