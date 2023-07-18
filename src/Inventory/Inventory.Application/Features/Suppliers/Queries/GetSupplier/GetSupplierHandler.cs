using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;
using Inventory.Core.Entities;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Queries.GetSupplier;

public class GetSupplierHandler : IRequestHandler<GetSupplierQuery, SupplierDto>
{
    private readonly ISupplyRepo _supplyRepo;
    private readonly ITenantService _tenantService;
    public GetSupplierHandler(ISupplyRepo supplyRepo, ITenantService tenantService)
    {
        _supplyRepo = supplyRepo;
        _tenantService = tenantService;
    }
    public async Task<SupplierDto> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
    {
        int tenantId = 1; //_tenantService.TenantId;
        int branchId = request.BranchId;
        var supplier = await _supplyRepo.GetSupplierById(request.SupplierId, tenantId, branchId, cancellationToken);

        return supplier is null
            ? default!
            : new SupplierDto(supplier.Id, supplier.Name, supplier.Email, supplier.PhoneNumbers, supplier.Address);
    }
}
