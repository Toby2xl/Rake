using System;

using FluentValidation;

namespace Inventory.Application.Features.Store.Commands.UpdateStore
{
    public class UpdateStoreValidator : AbstractValidator<UpdateStoreCommand>
    {
        public UpdateStoreValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Address).NotEmpty().NotNull();
        }
    }
}
