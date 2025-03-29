using Application.Core;
using Application.DTOs.Order;

namespace Application.Services;

public interface IOrderService
{
    Task<Result<string>> CreateAsync(CreateOrderDto dto);
    Task<Result<PagedResult<OrderDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<Result<PagedResult<OrderDto>>> GetByUserIdAsync(string userId, PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<Result<bool>> UpdateStatusAsync(UpdateOrderStatusDto dto);
}
