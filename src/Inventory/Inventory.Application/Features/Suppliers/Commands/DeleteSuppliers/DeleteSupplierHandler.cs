using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Commands.DeleteSuppliers;

public class DeleteSupplierHandler : IRequestHandler<DeleteSupplier, DeleteSupplyResponse>
{
    private readonly ISupplyRepo _supplyRepo;
    private readonly ITenantService _tenantService;
    public DeleteSupplierHandler(ISupplyRepo supplyRepo, ITenantService tenantService)
    {
        _supplyRepo = supplyRepo;
        _tenantService = tenantService;
    }
    public async Task<DeleteSupplyResponse> Handle(DeleteSupplier request, CancellationToken cancellationToken)
    {
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;
        var response = new DeleteSupplyResponse();
        var supplyToDelete = await _supplyRepo.GetSupplierById(request.SupplierId, tenantId, branchId, cancellationToken);

        if (supplyToDelete is null)
        {
            response.Success = false;
            response.Message = "Not Found";
            response.Data = "Not Found";
            return response;
        }

        await _supplyRepo.DeleteAsync(supplyToDelete, cancellationToken);
        response.Message = $"{supplyToDelete.Name} removed successfully.";
        response.Data = "Success";
        return response;
    }
}
