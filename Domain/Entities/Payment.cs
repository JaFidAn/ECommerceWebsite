using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public string OrderId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string TransactionId { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public PaymentStatus Status { get; set; }

    // Navigation
    public Order Order { get; set; } = null!;
}
