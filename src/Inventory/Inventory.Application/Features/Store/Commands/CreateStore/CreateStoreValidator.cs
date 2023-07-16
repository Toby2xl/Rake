using System;

using FluentValidation;

namespace Inventory.Application.Features.Store.Commands.CreateStore
{
    public class CreateStoreValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreValidator()
        {
            RuleFor(p => p.Name)
                   .NotEmpty().WithMessage("{PropertyName} is required.")
                   .NotNull()
                   .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");

            RuleFor(p => p.Description)
                    .NotEmpty().WithMessage("The {PropertyName} must not be empty")
                    .NotNull();

            RuleFor(p => p.Code)
                    .NotEmpty().WithMessage("The {PropertyName} must not be empty")
                    .NotNull();

            RuleFor(p => p.Address)
                    .NotEmpty().WithMessage("The {PropertyName} must not be empty")
                    .NotNull();

            RuleFor(p => p.BranchId)
                   .NotEmpty()
                   .NotEqual(0)
                   .GreaterThan(0)
                   .WithMessage("{PropertyName} is required.");
        }
    }
}
