using System;

using Inventory.Core.Entities;

namespace Inventory.Application.Repository;
public interface IStoreRepo
{
    Task<bool> AddWarehouseAsync(Warehouse store, CancellationToken ct);
    Task<IReadOnlyList<Warehouse>> GetAllStoresAsync(int tenantId, int branchId, CancellationToken ct);
    Task<IReadOnlyList<Warehouse>> GetAllStoresByBranchAsync(int tenantId, int branchId, CancellationToken ct);
    Task<string> GetWarehouseNameById(Guid storeId, int tenantId, CancellationToken ct);
    Task<Warehouse> GetWarehouseById(Guid storeId, int tenantId, int? branchId, CancellationToken ct);
    Task<Guid> GetWarehouseIdByName(string storeName, int tenantId, int branchId, CancellationToken ct);
    Task UpdateAsync(Warehouse entity, CancellationToken ct);
    Task<(bool, string)> DeleteAsync(Warehouse entity, CancellationToken ct);
}
