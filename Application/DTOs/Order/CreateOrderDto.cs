namespace Application.DTOs.Order;

public class CreateOrderDto
{
    public string CartId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string ShippingAddress { get; set; } = null!;
}
