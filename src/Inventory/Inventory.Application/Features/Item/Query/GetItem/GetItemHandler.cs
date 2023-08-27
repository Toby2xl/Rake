using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Item.Query.GetItem;

public class GetItemHandler : IRequestHandler<GetItemQuery, GetItemResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ITemsRepo _itemRepo;
    public GetItemHandler(ITenantService tenantService, ITemsRepo itemRepo)
    {
        _tenantService = tenantService;
        _itemRepo = itemRepo;
    }
    public async Task<GetItemResponse> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        var response = new GetItemResponse();
        int tenantId = _tenantService.TenantId;
        var item = await _itemRepo.GetSelectedItemByIdAsync(request.ItemId, request.StoreId, tenantId, request.BranchId);

        if (item is null)
        {
            response.Success = false;
            response.Data = null;
            response.Message = "Item does not exist.";
            return response;
        }

        response.Success = true;
        response.Data = new ItemDto
        {
            ItemId = item.ItemId,
            Name = item.Name,
            CostPrice = item.CostPrice,
            TotalCostPrice = item.ItemQuantity * item.CostPrice,
            ItemQuantity = item.ItemQuantity,
            Units = item.Units,
            IsForSale = item.IsForSale,
            Price = item.IsForSale ? item.Price : 0.00M,
            CategoryName = item.CategoryName,
            UPCNumber = item.UPCNumber,
        };
        return response;
    }
}
