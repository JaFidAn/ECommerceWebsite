using Application.Core;
using Application.DTOs.Order;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class OrdersController : BaseApiController
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var result = await _orderService.CreateAsync(dto);
        return HandleResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetAllAsync(paginationParams, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(string userId, [FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var result = await _orderService.GetByUserIdAsync(userId, paginationParams, cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus(UpdateOrderStatusDto dto)
    {
        var result = await _orderService.UpdateStatusAsync(dto);
        return HandleResult(result);
    }
}
