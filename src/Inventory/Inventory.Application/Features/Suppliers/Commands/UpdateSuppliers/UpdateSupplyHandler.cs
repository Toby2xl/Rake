using FluentValidation;

using Inventory.Application.Repository;
using Inventory.Application.Service;
using MediatR;

using Serilog;

namespace Inventory.Application.Features.Suppliers.Commands.UpdateSuppliers;

public class UpdateSupplyHandler : IRequestHandler<UpdateSupplier, UpdateSupplyResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ISupplyRepo _supplyRepo;
    private readonly IValidator<UpdateSupplier> _validator;
    public UpdateSupplyHandler(ITenantService tenantService, ISupplyRepo supplyRepo, IValidator<UpdateSupplier> validator)
    {
        _tenantService = tenantService;
        _supplyRepo = supplyRepo;
        _validator = validator;
    }
    public async Task<UpdateSupplyResponse> Handle(UpdateSupplier request, CancellationToken cancellationToken)
    {
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;
        var response = new UpdateSupplyResponse();
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

        Log.Information("BranchId:{@branchid}, SupplierId:{@supp}", branchId, request.SupplierId);
        var supplyToUpdate = await _supplyRepo.GetSupplierById(request.SupplierId, tenantId, branchId, cancellationToken);
        if(supplyToUpdate is null)
        {
            response.Success = false;
            response.Message = "Not Found";
            response.Data = null;
            return response;
        }
        Log.Information("supplierDetails => {@supplier}", supplyToUpdate);
        supplyToUpdate.UpdateSupplierDetails(request.Name, request.Email, request.PhoneNumbers, request.Address);
        await _supplyRepo.UpdateAsync(supplyToUpdate, cancellationToken);

        response.Message = "Success";
        response.Data = "Successfully updataed";
        return response;
    }
}
