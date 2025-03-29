using Application.DTOs.Order;
using FluentValidation;

namespace Application.Validators.Order;

public class UpdateOrderStatusValidator : AbstractValidator<UpdateOrderStatusDto>
{
    public UpdateOrderStatusValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId is required");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid OrderStatus");
    }
}
