using System;
using Inventory.Application.Repository;
using Inventory.Application.Service;
using MediatR;

namespace Inventory.Application.Features.Item.Query.GetItemList;

public class GetItemListHandler : IRequestHandler<GetItemListQuery, GetItemListResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ITemsRepo _itemRepo;
    public GetItemListHandler(ITenantService tenantService, ITemsRepo itemRepo)
    {
        _tenantService = tenantService;
        _itemRepo = itemRepo;
    }
    public async Task<GetItemListResponse> Handle(GetItemListQuery request, CancellationToken cancellationToken)
    {
        var response = new GetItemListResponse();
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;
        Guid storeId = request.StoreId;
        int cursor = request.Cursor;
        int pageSize = request.PageSize;

        var (nextCursor, allItems) = await _itemRepo.GetAllItemByStoreAsync(storeId, tenantId, branchId, cursor, pageSize,
                                                                            cancellationToken);
        if (allItems.Count < 1)
        {
            response.Success = false;
            response.Data = Enumerable.Empty<ItemsListVm>();
            response.Message = "No List of Items Found";
            response.NextCursor = null;
            return response;
        }
        var itemList = allItems.Select(c => new ItemsListVm(c.ItemId, c.Sn, c.Name, c.ItemQuantity, c.Units, c.UPCNumber!,
                                   c.CategoryName!, c.CostPrice, c.IsForSale, c.Price)).ToList();

        response.Success = true;
        response.NextCursor = nextCursor;
        response.Message = "success";
        response.Data = itemList;
        return response;

    }
}
