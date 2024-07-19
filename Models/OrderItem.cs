public class OrderItem
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public int ProductId { get; set; } // Foreign key for the related product
    public Product? Product { get; set; } // Navigation property
    public int OrderId { get; set; } // Foreign key for the related Order
    public Order? Order { get; set; } // Navigation property
}
