using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Queries.GetSupplierList
{
    public class GetSupplierListHandler : IRequestHandler<GetSupplierList, SupplierListResponse>
    {
        private readonly ISupplyRepo _supplyRepo;
        private readonly ITenantService _tenantService;
        public GetSupplierListHandler(ITenantService tenantService, ISupplyRepo supplyRepo)
        {
            _tenantService = tenantService;
            _supplyRepo = supplyRepo;
        }
        public async Task<SupplierListResponse> Handle(GetSupplierList request, CancellationToken cancellationToken)
        {
            int tenantId = 1;//_tenantService.TenantId;
            int branchId = request.BranchId;
            var response = new SupplierListResponse();

            var allsuppliers = await _supplyRepo.GetAllSuppliers(tenantId, branchId, cancellationToken);
            if (!allsuppliers.Any())
            {
                response.Success = false;
                response.Message = "No such List exixts";
                response.Data = Enumerable.Empty<SupplierListDto>();
                return response;
            }

            var supplierList = allsuppliers.Select(x => new SupplierListDto(x.Id, x.Name, x.Email, x.Address, x.PhoneNumbers, x.BranchId));
            response.Message = "Success";
            response.Data = supplierList;
            return response;
        }
    }
}
