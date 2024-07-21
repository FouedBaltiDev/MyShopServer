namespace MyShop.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyShop.Services;

[Route("api/[controller]")]
[ApiController]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _deliveryService;

    public DeliveriesController(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }

    [HttpGet("{orderId}")]
    public ActionResult<Delivery> GetDeliveryByOrderId(int orderId)
    {
        var delivery = _deliveryService.GetDeliveryByOrderId(orderId);
        if (delivery == null)
        {
            return NotFound();
        }
        return Ok(delivery);
    }

    [HttpPost]
    public ActionResult AddDelivery(Delivery delivery)
    {
        _deliveryService.AddDelivery(delivery);
        return CreatedAtAction(nameof(GetDeliveryByOrderId), new { orderId = delivery.OrderId }, delivery);
    }

    [HttpPut("{orderId}")]
    public ActionResult UpdateDeliveryStatus(int orderId, string status)
    {
        var delivery = _deliveryService.GetDeliveryByOrderId(orderId);
        if (delivery == null)
        {
            return NotFound();
        }

        _deliveryService.UpdateDeliveryStatus(orderId, status);
        return NoContent();
    }

    [HttpDelete("{orderId}")]
    public ActionResult DeleteDelivery(int orderId)
    {
        var delivery = _deliveryService.GetDeliveryByOrderId(orderId);
        if (delivery == null)
        {
            return NotFound();
        }

        _deliveryService.DeleteDelivery(orderId);
        return NoContent();
    }
}


