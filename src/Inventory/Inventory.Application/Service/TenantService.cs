using System;

using Inventory.Application.Repository;

namespace Inventory.Application.Service;

public class TenantService : ITenantService
{
    private readonly ITenantRepo _tenantRepo;
    public TenantService(ITenantRepo tenantRepo)
    {
        _tenantRepo = tenantRepo;
    }
    public string Tenant {get; private set;} = null!;

    public int TenantId => Tenant is null ? 0 : _tenantRepo.GetTenantIdByName(Tenant);

    public async Task<bool> IsTenantAvailableAsync(string tenantName, CancellationToken ct) => await _tenantRepo.IsTenantAvailableAsync(tenantName, ct);

    public void SetTenant(string tenant)
    {
        Tenant = tenant;
    }

    public async Task<int> TId(string tenantName, CancellationToken ct)
    {
        return await GetTenantIdAsync(tenantName, ct);
    }

    private async Task<int> GetTenantIdAsync(string tenantName, CancellationToken ct)
    {
        return await _tenantRepo.GetTenantIdByNameAsync(tenantName, ct);
    }
}
