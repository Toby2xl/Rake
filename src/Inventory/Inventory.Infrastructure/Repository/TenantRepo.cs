using System;

using Inventory.Application.Repository;
using Inventory.Core.Entities;
using Inventory.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository;

public class TenantRepo : ITenantRepo
{
    private readonly InventoryDbContext _context;
    public TenantRepo(InventoryDbContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyList<Branch>> GetBranchListByTenant(int tenantId, CancellationToken ct)
    {
        return await _context.Branches.Where(x => x.TenantId == tenantId).ToListAsync(ct);
    }

    public int GetTenantIdByName(string tenantName)
    {
        var tenant = _context.Tenants.Where(x => string.Equals(x.TenantName, tenantName)).SingleOrDefault();
        return tenant == null ? -1 : tenant.Id;
    }

    public async Task<int> GetTenantIdByNameAsync(string tenantName, CancellationToken ct)
    {
        var tenant = await _context.Tenants.Where(x => string.Equals(x.TenantName, tenantName)).SingleOrDefaultAsync(ct);
        return tenant == null ? -1 : tenant.Id;
    }

    public bool IsTenantAvailable(string tenantName)
    {
        return _context.Tenants.Any(x => string.Equals(x.TenantName, tenantName));
    }

    public async Task<bool> IsTenantAvailableAsync(string tenantName, CancellationToken ct)
    {
        return await _context.Tenants.AnyAsync(x => string.Equals(x.TenantName, tenantName), ct);
    }
}
