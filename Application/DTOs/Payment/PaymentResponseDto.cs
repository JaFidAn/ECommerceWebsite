namespace Application.DTOs.Payment;

public class PaymentResponseDto
{
    public string TransactionId { get; set; } = null!;
    public string Status { get; set; } = null!;
}
