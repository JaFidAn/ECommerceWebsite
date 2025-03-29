using Application.DTOs.Payment;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController : BaseApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(PaymentRequestDto dto)
    {
        var result = await _paymentService.ProcessPaymentAsync(dto);
        return HandleResult(result);
    }
}
