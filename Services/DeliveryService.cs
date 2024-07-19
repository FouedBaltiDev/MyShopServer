namespace MyShop.Services;

public class DeliveryService : IDeliveryService
{
    private readonly List<Delivery> deliveries;

    public DeliveryService()
    {
        // Initialiser une liste de livraisons pour simuler la base de données
        deliveries = new List<Delivery>();
    }

    public Delivery GetDeliveryByOrderId(int orderId)
    {
        return deliveries.FirstOrDefault(delivery => delivery.OrderId == orderId);
    }

    public void AddDelivery(Delivery delivery)
    {
        deliveries.Add(delivery);
    }

    public void UpdateDeliveryStatus(int orderId, string status)
    {
        var delivery = GetDeliveryByOrderId(orderId);
        if (delivery != null)
        {
            delivery.Status = status;
        }
    }

    public void DeleteDelivery(int orderId)
    {
        var delivery = GetDeliveryByOrderId(orderId);
        if (delivery != null)
        {
            deliveries.Remove(delivery);
        }
    }
}

