using Application.Core;
using Application.DTOs.Cart;

namespace Application.Services;

public interface ICartService
{
    Task<Result<CartDto>> GetCartAsync(string userId);
    Task<Result<string>> AddToCartAsync(string userId, AddToCartDto dto);
    Task<Result<bool>> RemoveFromCartAsync(string userId, string productId);
    Task<Result<decimal>> CalculateTotalPriceAsync(string userId);
}
