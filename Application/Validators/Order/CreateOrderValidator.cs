using Application.DTOs.Order;
using FluentValidation;

namespace Application.Validators.Order;

public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty().WithMessage("CartId is required");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("PaymentMethod is required");

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("ShippingAddress is required")
            .MaximumLength(250).WithMessage("ShippingAddress is too long");
    }
}
