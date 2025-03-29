using Application.Core;
using Application.DTOs.Cart;
using Application.Repositories.CartRepositories;
using Application.Repositories.ProductRepositories;
using Application.Services;
using Application.Utilities;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class CartService : ICartService
{
    private readonly ICartReadRepository _cartReadRepository;
    private readonly ICartWriteRepository _cartWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IMapper _mapper;

    public CartService(
        ICartReadRepository cartReadRepository,
        ICartWriteRepository cartWriteRepository,
        IProductReadRepository productReadRepository,
        IMapper mapper)
    {
        _cartReadRepository = cartReadRepository;
        _cartWriteRepository = cartWriteRepository;
        _productReadRepository = productReadRepository;
        _mapper = mapper;
    }

    public async Task<Result<CartDto>> GetCartAsync(string userId)
    {
        var cart = await _cartReadRepository
            .GetAll()
            .Include(x => x.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cart == null)
            return Result<CartDto>.Failure(MessageGenerator.NotFound("Cart"), 404);

        var dto = _mapper.Map<CartDto>(cart);
        return Result<CartDto>.Success(dto);
    }

    public async Task<Result<string>> AddToCartAsync(string userId, AddToCartDto dto)
    {
        var product = await _productReadRepository.GetByIdAsync(dto.ProductId);
        if (product == null)
            return Result<string>.Failure(MessageGenerator.NotFound("Product"), 404);

        var cart = await _cartReadRepository
            .GetAll()
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem>()
            };
            await _cartWriteRepository.AddAsync(cart);
        }

        var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == dto.ProductId);
        if (cartItem != null)
        {
            cartItem.Quantity += dto.Quantity;
        }
        else
        {
            cart.CartItems.Add(new CartItem
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                Price = product.Price
            });
        }

        await _cartWriteRepository.SaveAsync();
        return Result<string>.Success(cart.Id, MessageGenerator.CreationSuccess("Cart item"));
    }

    public async Task<Result<bool>> RemoveFromCartAsync(string userId, string productId)
    {
        var cart = await _cartReadRepository
            .GetAll()
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cart == null)
            return Result<bool>.Failure(MessageGenerator.NotFound("Cart"), 404);

        var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
        if (cartItem == null)
            return Result<bool>.Failure("Product not found in cart", 404);

        cart.CartItems.Remove(cartItem);
        await _cartWriteRepository.SaveAsync();

        return Result<bool>.Success(true, MessageGenerator.DeletionSuccess("Cart item"));
    }

    public async Task<Result<decimal>> CalculateTotalPriceAsync(string userId)
    {
        var cart = await _cartReadRepository
            .GetAll()
            .Include(x => x.CartItems)
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (cart == null)
            return Result<decimal>.Failure(MessageGenerator.NotFound("Cart"), 404);

        var total = cart.CartItems.Sum(x => x.Price * x.Quantity);
        return Result<decimal>.Success(total);
    }
}
