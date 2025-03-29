using Application.DTOs.Cart;
using FluentValidation;

namespace Application.Validators.Cart;

public class AddToCartValidator : AbstractValidator<AddToCartDto>
{
    public AddToCartValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("ProductId is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0");
    }
}
