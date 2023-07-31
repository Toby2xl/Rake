using FluentValidation;

using Inventory.Application.Repository;
using Inventory.Application.Service;
using Inventory.Core.Entities;
using Inventory.Core.ValueObject;

using MediatR;

namespace Inventory.Application;

public class CreateItemHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly ITenantService _tenantService;
    private readonly IValidator<CreateItemCommand> _validator;
    private readonly ITemsRepo _itemsRepo;
    private readonly ICategoryService _categoryService;

    public CreateItemHandler(IValidator<CreateItemCommand> validator, ITemsRepo itemsRepo, ITenantService tenantService,
                             ICategoryService categoryService)
    {
        _validator = validator;
        _itemsRepo = itemsRepo;
        _tenantService = tenantService;
        _categoryService = categoryService;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateItemResponse();
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;

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

        //Check whether the category name exists in the db for that TenantId and branchId
        var(categoryExists, categoryId) = await _categoryService.DoesCategoryExistAsync(request.CategoryName, tenantId, branchId);

        //Create The Item ....
        var newCreatedItem = Item.CreateNewItem(request.Name, request.Description, request.Unit, request.StoreId,
                                                tenantId, request.CostPrice, request.IsForSale,
                                                request.UnitPrice, branchId);
        if(categoryExists)
        {
            newCreatedItem.AppendExistingCategory(categoryId);
        }
        else
        {
            newCreatedItem.AddNewCategory(request.CategoryName);
        }
        
        //Append the QunatityDetails to the ItemDto
        var  createdItemToAdd = CreateItemToAdd(newCreatedItem, request.QtyDetails);

        var (isSuccess, created) = await _itemsRepo.AddStockItemAsync(createdItemToAdd);
        if(!isSuccess && created is not null)
        {
            response.Success = false;
            response.Data = null;
            response.Message = created.Message;
            return response;
        }
        if (!isSuccess && created is null)
        {
            response.Success = false;
            response.Data = null;
            response.Message = "The Item couldn't be created";
            return response;
        }
        var createItem = new CreateItemDto(created!.Id, created.Name, created.CostPrice,
                                           created.Quantity, created.UnitPrice, created.IsForSale,created.CategoryName,
                                            created.UPCNumber,created.Message);
        response.Data = createItem;
        response.Message = "Item Created successfully....";
        return response;
    }

    private static ItemDto CreateItemToAdd(Item newItem, QuantityDetails qtyDetails)
    {
        return new ItemDto
        {
            NewItem = newItem,
            QuantityDetails = qtyDetails
        };
    }

}
