using System;

using Inventory.Application.DbDto;
using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Item.Commands.DeleteItem;

public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, DeleteResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ITemsRepo _itemRepo;
    public DeleteItemHandler(ITemsRepo itemRepo, ITenantService tenantService)
    {
        _itemRepo = itemRepo;
        _tenantService = tenantService;
    }

    public async Task<DeleteResponse> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        var response = new DeleteResponse();
        int tenantId = _tenantService.TenantId;
        var itemToDelete = await _itemRepo.GetItemByIdAsync(request.ItemId, tenantId, request.BranchId, cancellationToken);

        if(itemToDelete is null)
        {
            response.Success = false;
            response.Data = "The Item resource was not found.";
            return response;
        }
        var itemDelete = new ItemDeleteDto
        {
            StoreId = request.StoreId,
            ItemId = request.ItemId,
            TenantId = tenantId,
            BranchId = request.BranchId,
        };
        await _itemRepo.DeleteAsync(itemDelete, cancellationToken);

        response.Data = "Item deleted successfully";
        return response;
    }
}
