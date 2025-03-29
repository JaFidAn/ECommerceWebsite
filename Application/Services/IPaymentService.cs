using Application.Core;
using Application.DTOs.Payment;

namespace Application.Services;

public interface IPaymentService
{
    Task<Result<PaymentResponseDto>> ProcessPaymentAsync(PaymentRequestDto dto);
}
