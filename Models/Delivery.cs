public class Delivery
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Address { get; set; }
    public string? Status { get; set; }
}
