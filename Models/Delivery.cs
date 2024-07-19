using MyShop.Models;

public class Delivery
{
    public int Id { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Address { get; set; }
    public string? Status { get; set; }

    public string? UserId { get; set; }  // Foreign key (User => Delivery)
    public User? User { get; set; } // Navigation property

    public int OrderId { get; set; } // Foreign key (order => Delivery)
    public Order? Order { get; set; } // Navigation property for the related Order (One to One)
}
