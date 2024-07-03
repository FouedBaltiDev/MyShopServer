public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderItem>? CartItems { get; set; }
}
