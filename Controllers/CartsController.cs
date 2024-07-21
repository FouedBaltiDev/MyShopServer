namespace MyShop.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyShop.Services;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public ActionResult<Cart> GetCartByUserId(string userId)
    {
        var cart = _cartService.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound();
        }
        return Ok(cart);
    }

    [HttpPost("{userId}/items")]
    public ActionResult AddItemToCart(string userId, OrderItem item)
    {
        _cartService.AddItemToCart(userId, item);
        return NoContent();
    }

    [HttpDelete("{userId}/items/{itemId}")]
    public ActionResult RemoveItemFromCart(string userId, int itemId)
    {
        _cartService.RemoveItemFromCart(userId, itemId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public ActionResult ClearCart(string userId)
    {
        _cartService.ClearCart(userId);
        return NoContent();
    }
}


