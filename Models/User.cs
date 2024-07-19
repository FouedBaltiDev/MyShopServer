using Microsoft.AspNetCore.Identity;

namespace MyShop.Models;

public class User : IdentityUser
{
    public Cart? Cart { get; set; }  // Navigation property for the related Cart (One to One)

    public List<Order> Orders { get; set; } = new(); // User peut avoir 1 ou * Order

    public List<Delivery> Deliveries { get; set; } = new(); // User peut avoir 1 ou * Delivery
}

