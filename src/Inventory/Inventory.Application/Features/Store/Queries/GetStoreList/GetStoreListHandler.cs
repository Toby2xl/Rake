using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

namespace Inventory.Application.Features.Store.Queries.GetStoreList;

public class GetStoreListHandler : IRequestHandler<GetStoreListQuery, List<StoreListVm>>
{
    private readonly IStoreRepo _storeRepo;
    private readonly ITenantService _tenantService;
    public GetStoreListHandler(IStoreRepo storeRepo, ITenantService tenantService)
    {
        _storeRepo = storeRepo;
        _tenantService = tenantService;
    }
    public async Task<List<StoreListVm>> Handle(GetStoreListQuery request, CancellationToken cancellationToken)
    {
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;

        var allStores = await _storeRepo.GetAllStoresAsync(tenantId, branchId, cancellationToken);
        return allStores.Count > 1
            ? allStores.Select(x => new StoreListVm(x.Id, x.Name, x.Description, x.Code, x.Address, x.BranchId)).ToList()
            : Enumerable.Empty<StoreListVm>().ToList();
    }

}
