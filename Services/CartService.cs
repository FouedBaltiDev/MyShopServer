namespace MyShop.Services;

public class CartService : ICartService
{
    private readonly List<Cart> carts;

    public CartService()
    {
        // Initialiser une liste de paniers pour simuler la base de données
        carts = new List<Cart>();
    }

    public Cart GetCartByUserId(string userId)
    {
        return carts.FirstOrDefault(cart => cart.UserId == userId) ?? new Cart { UserId = userId, CartItems = new List<OrderItem>() };
    }

    public void AddItemToCart(string userId, OrderItem item)
    {
        var cart = GetCartByUserId(userId);
        if (cart.CartItems == null)
        {
            cart.CartItems = new List<OrderItem>();
        }
        cart.CartItems.Add(item);
        if (!carts.Any(c => c.UserId == userId))
        {
            carts.Add(cart);
        }
    }

    public void RemoveItemFromCart(string userId, int itemId)
    {
        var cart = GetCartByUserId(userId);
        if (cart.CartItems != null)
        {
            var item = cart.CartItems.FirstOrDefault(i => i.Id == itemId);
            if (item != null)
            {
                cart.CartItems.Remove(item);
            }
        }
    }

    public void ClearCart(string userId)
    {
        var cart = GetCartByUserId(userId);
        if (cart.CartItems != null)
        {
            cart.CartItems.Clear();
        }
    }
}

