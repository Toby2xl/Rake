using System;

using Inventory.Core.Entities;

namespace Inventory.Application.Repository;

public interface ITenantRepo
{
    int GetTenantIdByName(string tenantName);

    bool IsTenantAvailable(string tenantName);

    Task<int> GetTenantIdByNameAsync(string tenantName, CancellationToken ct);

    Task<bool> IsTenantAvailableAsync(string tenantName, CancellationToken ct);

    Task<IReadOnlyList<Branch>> GetBranchListByTenant(int tenantId, CancellationToken ct);
}
