using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.DeleteStore;

public class DeleteStoreCommandHandler : IRequestHandler<DeleteStoreCommand, DeleteResponse>
{
    private readonly ITenantService _tenantService;
    private readonly IStoreRepo _storeRepo;
    public DeleteStoreCommandHandler(ITenantService tenantService, IStoreRepo storeRepo)
    {
        _tenantService = tenantService;
        _storeRepo = storeRepo;
    }
    public async Task<DeleteResponse> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteResponse();
        int tenantId = 1;
        var storeToDelete = await _storeRepo.GetWarehouseById(request.StoreId, tenantId, request.BranchId, cancellationToken);
        if (storeToDelete is null)
        {
            //throw new NotFoundException(nameof(Warehouse), request.WarehouseId);
            response.Success = false;
            response.Data = null;
            response.Message = "No such store exists.";
            return response;
        }

        var (isStoreEmpty, message) = await _storeRepo.DeleteAsync(storeToDelete, cancellationToken);
        
        if (!isStoreEmpty)
        {
            response.Success = isStoreEmpty;
            response.Data = message;
            response.Message = message;
            return response;
        }
        response.Success = isStoreEmpty;
        response.Data = message;
        response.Message = message;
        return response;
    }
}
