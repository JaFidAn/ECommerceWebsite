using Application.DTOs.Cart;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartsController : BaseApiController
{
    private readonly ICartService _cartService;

    public CartsController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(string userId)
    {
        var result = await _cartService.GetCartAsync(userId);
        return HandleResult(result);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> AddToCart(string userId, AddToCartDto dto)
    {
        var result = await _cartService.AddToCartAsync(userId, dto);
        return HandleResult(result);
    }

    [HttpDelete("{userId}/{productId}")]
    public async Task<IActionResult> RemoveFromCart(string userId, string productId)
    {
        var result = await _cartService.RemoveFromCartAsync(userId, productId);
        return HandleResult(result);
    }

    [HttpGet("{userId}/total-price")]
    public async Task<IActionResult> CalculateTotalPrice(string userId)
    {
        var result = await _cartService.CalculateTotalPriceAsync(userId);
        return HandleResult(result);
    }
}
