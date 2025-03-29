using Application.Core;
using Application.DTOs.Order;
using Application.Repositories.CartRepositories;
using Application.Repositories.OrderRepositories;
using Application.Services;
using Application.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class OrderService : IOrderService
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly ICartReadRepository _cartReadRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderWriteRepository orderWriteRepository,
        IOrderReadRepository orderReadRepository,
        ICartReadRepository cartReadRepository,
        IMapper mapper)
    {
        _orderWriteRepository = orderWriteRepository;
        _orderReadRepository = orderReadRepository;
        _cartReadRepository = cartReadRepository;
        _mapper = mapper;
    }

    public async Task<Result<string>> CreateAsync(CreateOrderDto dto)
    {
        var cart = await _cartReadRepository
            .GetAll()
            .Include(x => x.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(x => x.Id == dto.CartId && x.UserId == dto.UserId);

        if (cart == null || !cart.CartItems.Any())
            return Result<string>.Failure("Cart is empty or not found", 404);

        var totalPrice = cart.CartItems.Sum(item => item.Quantity * item.Price);

        var order = new Order
        {
            UserId = dto.UserId,
            TotalPrice = totalPrice,
            PaymentMethod = dto.PaymentMethod,
            ShippingAddress = dto.ShippingAddress,
            OrderItems = cart.CartItems.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                Price = item.Price
            }).ToList()
        };

        await _orderWriteRepository.AddAsync(order);

        // Clear cart
        cart.CartItems.Clear();
        await _orderWriteRepository.SaveAsync();

        return Result<string>.Success(order.Id, "Order created successfully");
    }

    public async Task<Result<PagedResult<OrderDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var query = _orderReadRepository
            .GetAll()
            .Include(x => x.OrderItems)
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

        var pagedResult = await PagedResult<OrderDto>.CreateAsync(
            query,
            paginationParams.PageNumber,
            paginationParams.PageSize,
            cancellationToken
        );

        return Result<PagedResult<OrderDto>>.Success(pagedResult);
    }

    public async Task<Result<PagedResult<OrderDto>>> GetByUserIdAsync(string userId, PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var query = _orderReadRepository
            .GetAll()
            .Where(x => x.UserId == userId)
            .Include(x => x.OrderItems)
            .OrderByDescending(x => x.CreatedAt)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider);

        var pagedResult = await PagedResult<OrderDto>.CreateAsync(
            query,
            paginationParams.PageNumber,
            paginationParams.PageSize,
            cancellationToken
        );

        return Result<PagedResult<OrderDto>>.Success(pagedResult);
    }

    public async Task<Result<bool>> UpdateStatusAsync(UpdateOrderStatusDto dto)
    {
        var order = await _orderReadRepository.GetByIdAsync(dto.OrderId);
        if (order == null)
            return Result<bool>.Failure(MessageGenerator.NotFound("Order"), 404);

        order.Status = dto.Status;
        _orderWriteRepository.Update(order);
        await _orderWriteRepository.SaveAsync();

        return Result<bool>.Success(true, MessageGenerator.UpdateSuccess("Order status"));
    }
}
