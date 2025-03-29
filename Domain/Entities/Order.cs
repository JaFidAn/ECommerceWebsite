using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public string UserId { get; set; } = null!;
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string ShippingAddress { get; set; } = null!;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    // Navigation
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
