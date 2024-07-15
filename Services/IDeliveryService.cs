namespace MyShop.Services;

public interface IDeliveryService
{
    Delivery GetDeliveryByOrderId(int orderId);
    void AddDelivery(Delivery delivery);
    void UpdateDeliveryStatus(int orderId, string status);
    void DeleteDelivery(int orderId);
}

