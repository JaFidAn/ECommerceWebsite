namespace Application.DTOs.Payment;

public class PaymentRequestDto
{
    public string OrderId { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string Token { get; set; } = null!;
}
