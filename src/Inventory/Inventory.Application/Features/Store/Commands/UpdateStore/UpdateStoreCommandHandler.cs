using System;

using FluentValidation;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.UpdateStore
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, UpdateStoreResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly IStoreRepo _storeRepo;
        private readonly IValidator<UpdateStoreCommand> _validator;
        public UpdateStoreCommandHandler(ITenantService tenantService, IStoreRepo storeRepo, IValidator<UpdateStoreCommand> validator)
        {
            _tenantService = tenantService;
            _storeRepo = storeRepo;
            _validator = validator;
        }
        public async Task<UpdateStoreResponse> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateStoreResponse();
            //int tenantId = _tenantService.TenantId;
            const int tenantId = 1;

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
                return response;
            }
            var storeToUpdate = await _storeRepo.GetWarehouseById(request.StoreId, tenantId, request.BranchId, cancellationToken);
            if (storeToUpdate is null)
            {
                response.Success = false;
                response.Message = "Couldn't update store";
                return response;
            }

            storeToUpdate.UpdateStoreDetails(request.Name, request.Description, request.Code, request.Address);
            await _storeRepo.UpdateAsync(storeToUpdate, cancellationToken);
            response.Success = true;
            response.Message = "Successful";
            return response;
        }
    }
}
