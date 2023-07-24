using FluentValidation;
using Inventory.Application.Repository;
using Inventory.Application.Service;
using Inventory.Core.Entities;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.CreateStore;

public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, CreateStoreResponse>
{
    private readonly ITenantService _tenantService;
    private readonly IValidator<CreateStoreCommand> _validator;
    private readonly IStoreRepo _storeRepo;
    public CreateStoreCommandHandler(ITenantService tenantService, IValidator<CreateStoreCommand> validator, IStoreRepo storeRepo)
    {
        _tenantService = tenantService;
        _validator = validator;
        _storeRepo = storeRepo;
    }
    public async Task<CreateStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
    {
        var response = new CreateStoreResponse();
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
        var warehouse = Warehouse.CreateNewStore(request.Name, request.Description, request.Code, request.Address, tenantId, branchId);
        var created = await _storeRepo.AddWarehouseAsync(warehouse, cancellationToken);
        if (!created)
        {
            response.Success = false;
            response.Data = null;
            response.Message = "Warehouse already exists.....";
            return response;
        }
        var createStore = new CreateStoreDto(warehouse.Id, warehouse.Name, warehouse.Description, warehouse.Code, warehouse.Address);
        response.Data = createStore;
        response.Message = $"Warehouse with Name {createStore.Name} created successfully";
        return response;
    }
}
