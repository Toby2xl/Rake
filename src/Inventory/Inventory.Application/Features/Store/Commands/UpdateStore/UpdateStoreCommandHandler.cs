using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.UpdateStore
{
    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand, UpdateStoreResponse>
    {
        private readonly ITenantService _tenantService;
        private readonly IStoreRepo _storeRepo;
        public UpdateStoreCommandHandler(ITenantService tenantService, IStoreRepo storeRepo)
        {
            _tenantService = tenantService;
            _storeRepo = storeRepo;
        }
        public async Task<UpdateStoreResponse> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateStoreResponse();
            int tenantId = 1;
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
