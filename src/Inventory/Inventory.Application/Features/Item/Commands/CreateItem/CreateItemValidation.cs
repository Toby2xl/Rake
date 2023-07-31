using FluentValidation;

namespace Inventory.Application;

public class CreateItemValidation : AbstractValidator<CreateItemCommand>
{
    public CreateItemValidation()
    {
        RuleFor(p => p.Name)
                   .NotEmpty().WithMessage("{PropertyName} is required.")
                   .NotNull();

        RuleFor(p => p.Unit)
                  .NotEmpty().WithMessage("The {PropertyName} is required.")
                  .Must(c => c.Length <= 8).WithMessage("The {PropertyName} must not exceed 5 characters")
                  .NotNull();

        RuleFor(p => p.Quantity)
                   .PrecisionScale(6, 1, false).WithMessage("The {PropertyName} should have only one decimal place (of '0.5')")
                   .Must(x => (x * 10) % 5 == 0).WithMessage("The {PropertyName} should be a whole number or having decimal of 0.5")
                   .NotEqual(0)
                   .GreaterThan(0).WithMessage("The {PropertyName} must not be Zero");

        RuleFor(p => p.CategoryName)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(p => p.CostPrice)
                   .PrecisionScale(18, 2, true)
                   .NotEqual(0.0M)
                   .GreaterThan(0.0M)
                   .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.UnitPrice)
        .NotEqual(0).When(x => x.IsForSale)
        .WithMessage("{PropertyName} must not be equal to Zero for a Saleable Item");

        RuleFor(p => p.BranchId)
                   .NotEmpty()
                   .NotEqual(0)
                   .GreaterThan(0)
                   .WithMessage("The {PropertyName} is required.");
    }
}
