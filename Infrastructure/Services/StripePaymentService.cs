using Application.Core;
using Application.DTOs.Payment;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Stripe;

namespace Infrastructure.Services;

public class StripePaymentService : IPaymentService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public StripePaymentService(IConfiguration configuration, ApplicationDbContext context)
    {
        _configuration = configuration;
        _context = context;

        StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
    }

    public async Task<Result<PaymentResponseDto>> ProcessPaymentAsync(PaymentRequestDto dto)
    {
        try
        {
            var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == dto.OrderId);
            if (order == null)
                return Result<PaymentResponseDto>.Failure("Order not found", 404);

            var amountInCents = (long)(order.TotalPrice * 100);

            var chargeOptions = new ChargeCreateOptions
            {
                Amount = amountInCents,
                Currency = "usd",
                Description = $"Order #{order.Id}",
                Source = dto.Token // 👈 Stripe token from frontend or test value
            };

            var chargeService = new ChargeService();
            var charge = await chargeService.CreateAsync(chargeOptions);

            var payment = new Payment
            {
                OrderId = order.Id,
                Amount = order.TotalPrice,
                TransactionId = charge.Id,
                PaymentMethod = dto.PaymentMethod,
                Status = charge.Status == "succeeded" ? PaymentStatus.Success : PaymentStatus.Failed
            };

            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            var response = new PaymentResponseDto
            {
                TransactionId = payment.TransactionId,
                Status = payment.Status.ToString()
            };

            return Result<PaymentResponseDto>.Success(response, "Payment processed successfully");
        }
        catch (Exception ex)
        {
            return Result<PaymentResponseDto>.Failure($"Payment failed: {ex.Message}", 500);
        }
    }
}
