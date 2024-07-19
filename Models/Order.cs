using MyShop.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Status { get; set; }
    public List<OrderItem>? OrderItems { get; set; }

    public string? UserId { get; set; }  // Foreign key (User => Order)
    public User? User { get; set; } // Navigation property


    public Delivery? Delivery { get; set; } // Navigation property for the related Delivery (One to One)

}
