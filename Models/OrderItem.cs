public class OrderItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    // Navigation property
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int OrderId { get; set; }
    // Navigation property
    public Order? Order { get; set; }
}
