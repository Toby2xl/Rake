using System;

using Inventory.Application.DbDto;
using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Item.Commands.UpdateItem;

public class UpdateItemHandler : IRequestHandler<UpdateItemCommand, UpdateItemResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ITemsRepo _itemRepo;
    private readonly ICategoryService _categoryService;
    public UpdateItemHandler(ITenantService tenantService, ITemsRepo itemRepo, ICategoryService categoryService)
    {
        _tenantService = tenantService;
        _itemRepo = itemRepo;
        _categoryService = categoryService;
    }
    public async Task<UpdateItemResponse> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateItemResponse();
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;

        //TODO: Add a validator class and don't trust the FrontEnd..........
        var itemToUpdate = await _itemRepo.GetItemByIdAsync(request.ItemId, tenantId, request.BranchId, cancellationToken);

        if(itemToUpdate is null)
        {
            response.Success = false;
            response.Data = "Item doesn't exist";
            return response;
        }
        var (_, categoryId) = await _categoryService.DoesCategoryExistAsync(request.CategoryName, tenantId, branchId);
        var itemUpdate = new ItemUpdateDto
        {
            StoreId = request.StoreId,
            ItemId = request.ItemId,
            Quantity = request.ItemQuantity,
            //ItemUpdate = itemToUpdate,

            Name = request.Name,
            Unit = request.Unit,
            CostPrice = request.CostPrice,
            IsForSale = request.IsForSale,
            UnitPrice = request.UnitPrice,
            CategoryId = categoryId
        };

        await _itemRepo.UpdateItemAsync(itemUpdate, tenantId, request.BranchId, cancellationToken);

        response.Data ="Item updated succesfully";
        return response;
    }
}
