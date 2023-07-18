using System;

using FluentValidation;

namespace Inventory.Application.Features.Suppliers.Commands.UpdateSuppliers
{
    public class UpdateSupplyValidator : AbstractValidator<UpdateSupplier>
    {
        public UpdateSupplyValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.PhoneNumbers)
                .NotNull()
                .WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.BranchId)
                   .NotEmpty()
                   .NotEqual(0)
                   .GreaterThan(0)
                   .WithMessage("{PropertyName} is required.");
        }
    }
}
