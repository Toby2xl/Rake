using System;

using Inventory.Application.Repository;
using Inventory.Application.Service;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Inventory.Application.Features.Store.Queries.GetStore;

public class GetStoreQueryHandler : IRequestHandler<GetStoreQuery, StoreDto>
{
    private readonly IStoreRepo _storeRepo;
    private readonly ITenantService _tenantService;
    private readonly ILogger<GetStoreQueryHandler> _logger;
    public GetStoreQueryHandler(IStoreRepo storeRepo, ITenantService tenantService, ILogger<GetStoreQueryHandler> logger)
    {
        _storeRepo = storeRepo;
        _tenantService = tenantService;
        _logger = logger;
    }
    public async Task<StoreDto> Handle(GetStoreQuery request, CancellationToken cancellationToken)
    {
        int tenantId = _tenantService.TenantId;
        int branchId = request.BranchId;
        var warehouse = await _storeRepo.GetWarehouseById(request.StoreId, tenantId, branchId, cancellationToken);

        if (warehouse is null)
        {
            //_logger.LogWarning($"The requested resource --> {nameof(warehouse)} returns null");
            return default!;
        }
        return new StoreDto(warehouse.Name, warehouse.Description, warehouse.Code, warehouse.Address);
    }
}
