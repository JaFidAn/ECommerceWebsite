using Domain.Enums;

namespace Application.DTOs.Order;

public class OrderDto
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string ShippingAddress { get; set; } = null!;
    public OrderStatus Status { get; set; }
    public List<OrderItemDto> OrderItems { get; set; } = new();
}
