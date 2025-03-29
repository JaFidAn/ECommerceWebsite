using Domain.Enums;

namespace Application.DTOs.Order;

public class UpdateOrderStatusDto
{
    public string OrderId { get; set; } = null!;
    public OrderStatus Status { get; set; }
}
