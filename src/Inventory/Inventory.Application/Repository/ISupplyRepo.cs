using System;

using Inventory.Core.Entities;

namespace Inventory.Application.Repository;
public interface ISupplyRepo
{
    Task<bool> AddSupplierAsync(Supplier supplier, CancellationToken ct);

    Task<Supplier> GetSupplierById(Guid supplierId, int tenantId, int branchId, CancellationToken ct);
    Task<IEnumerable<Supplier>> GetAllSuppliers(int tenantId, int branchId, CancellationToken ct);

    Task UpdateAsync(Supplier entity, CancellationToken ct);
    Task DeleteAsync(Supplier entity, CancellationToken ct);
}
