using System;

using Inventory.Application.Repository;

namespace Inventory.Application.Service;

public class TenantService : ITenantService
{
    private readonly ITenantRepo _tenantRepo;

    private static readonly Dictionary<string, int> TenantDict = new()
    {
        ["havard"] = 1,
        ["blueskies"] = 2,
        ["toby"] = 3
    };
    public TenantService(ITenantRepo tenantRepo)
    {
        _tenantRepo = tenantRepo;
    }
    public string Tenant {get; private set;} = string.Empty;

    //public int TenantId => Tenant is null ? 0 : _tenantRepo.GetTenantIdByName(Tenant);
    public int TenantId => Tenant is null ? 0 : TenantDict[Tenant];

    public async Task<bool> IsTenantAvailableAsync(string tenantName, CancellationToken ct)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(0.5), ct);
        return TenantDict.ContainsKey(tenantName);
        //return await _tenantRepo.IsTenantAvailableAsync(tenantName, ct);
    }

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
