namespace MyShop.Services;

public interface ICartService
{
    Cart GetCartByUserId(string userId);
    void AddItemToCart(string userId, OrderItem item);
    void RemoveItemFromCart(string userId, int itemId);
    void ClearCart(string userId);
}

