using MyShop.Models;

public class Cart
{
    public int Id { get; set; }
    public List<OrderItem>? CartItems { get; set; }

    public string? UserId { get; set; }  // Foreign key for the related User
    public User? User { get; set; } // Navigation property for the related User (One to One)
}
