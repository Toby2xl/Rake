using System;

using Inventory.Application.Repository;
using Inventory.Core.Entities;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repository;

public class SupplyRepo : ISupplyRepo
{
    private readonly InventoryDbContext _context;
    public SupplyRepo(InventoryDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddSupplierAsync(Supplier supplier, CancellationToken ct)
    {
        var supplierName = supplier.Name.ToLower();
        if (await _context.Suppliers.AnyAsync(x => x.Name.ToLower() == supplierName &&
                                              x.TenantId == supplier.TenantId &&
                                              x.BranchId == supplier.BranchId, ct))
        {
            return false;
        }

        await _context.Suppliers.AddAsync(supplier, ct);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IEnumerable<Supplier>> GetAllSuppliers(int tenantId, int branchId, CancellationToken ct)
    {
        var suppList = await _context.Suppliers.Where(x => x.TenantId == tenantId && x.BranchId == branchId).AsNoTracking().ToListAsync(ct);
        return suppList.Count < 1 ? Enumerable.Empty<Supplier>() : suppList;
    }

    public async Task<Supplier> GetSupplierById(Guid supplierId, int tenantId, int branchId, CancellationToken ct)
    {
        var supplierDetails = await _context.Suppliers
                                                    .Where(x => x.Id == supplierId && x.TenantId == tenantId && x.BranchId == branchId)
                                                    .FirstOrDefaultAsync(ct);
        return supplierDetails ?? default!;
    }

    public async Task UpdateAsync(Supplier entity, CancellationToken ct)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Supplier entity, CancellationToken ct)
    {
        _context.Set<Supplier>().Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
