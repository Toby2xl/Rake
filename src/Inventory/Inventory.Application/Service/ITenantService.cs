using System;

namespace Inventory.Application.Service;

public interface ITenantService
{
    string Tenant { get; }
    int TenantId { get; }

    Task<int> TId(string tenantName, CancellationToken ct);

    void SetTenant(string tenant);

    Task<bool> IsTenantAvailableAsync(string tenantName, CancellationToken ct);
}
