    using Inventory.Application.Repository;
    using Inventory.Core.Entities;
    using Inventory.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure.Repository;

public class StoreRepo2
{
            private readonly InventoryDbContext _context;

            private readonly ILogger<StoreRepo2> _logger;
            public StoreRepo2(InventoryDbContext context, ILogger<StoreRepo2> logger)
            {
                _context = context;
                _logger = logger;
            }
            public async Task<bool> AddWarehouseAsync(Warehouse store, CancellationToken ct)
            {
                if(await _context.Warehouses
                                .AnyAsync(x => x.Name == store.Name &&
                                                x.TenantId == store.TenantId &&
                                                x.BranchId == store.BranchId, cancellationToken: ct))
                {
                    return false;
                }
                //Add the warehouse to the Database......
                await _context.Warehouses.AddAsync(store, ct);
                await _context.SaveChangesAsync(ct);
                return true;
            }

            public async Task<IReadOnlyList<Warehouse>> GetAllStoresAsync(int tenantId, int branchId, CancellationToken ct)
            {
                return await _context.Warehouses.Where(x => x.TenantId == tenantId && x.BranchId == branchId)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken: ct);
            }

            public async Task<string> GetWarehouseNameById(Guid storeId, int tenantId, CancellationToken ct)
            {
                var storeName = await _context.Warehouses.AsNoTracking()
                                            .Where(x => x.Id == storeId && x.TenantId == tenantId)
                                            .SingleOrDefaultAsync(cancellationToken: ct);
                return storeName is null ? String.Empty : storeName.Name;
            }

            public async Task<Warehouse> GetWarehouseById(Guid storeId, int tenantId, int? branchId, CancellationToken ct)
            {
                var warehouse = await _context.Warehouses.AsNoTracking().Where(x => x.Id == storeId
                                                                && x.TenantId == tenantId
                                                                && x.BranchId == branchId).SingleOrDefaultAsync(ct);

                return warehouse ?? default!;
            }

            public async Task<Guid> GetWarehouseIdByName(string storeName, int tenantId, int branchId, CancellationToken ct)
            {
                if (!await _context.Warehouses.AnyAsync(x => x.Name == storeName, cancellationToken: ct))
                    return Guid.Empty;
                var warehouseId = await _context.Warehouses.AsNoTracking().Where(x => x.Name == storeName
                                                                && x.TenantId == tenantId
                                                                && x.BranchId == branchId)
                                                                .Select(c => c.Id).SingleOrDefaultAsync(ct);

                return warehouseId;
            }

            public async Task UpdateAsync(Warehouse entity, CancellationToken ct)
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync(ct);
            }

            private async Task<bool> IsItemInStore(Guid storeId, int tenantId, int branchId)
            {
                var itemInStore = await _context.Items.AsNoTracking().AnyAsync( x=> x.WarehouseId == storeId && x.TenantId == tenantId && x.BranchId ==branchId);
            // var itmesInStore = await _context.Items.AsNoTracking().Where(x => x.WarehouseId == storeId && x.TenantId == tenantId && x.BranchId == branchId).ToListAsync();
            // return itmesInStore is null;
            //var itemsInStore = _context.Items.AsNoTracking().Where(x => x.WarehouseId == storeId && x.TenantId == tenantId && x.BranchId == branchId).Any();
            return itemInStore;
            }

            public async Task<(bool, string)> DeleteAsync(Warehouse entity, CancellationToken ct)
            {
                var storeId = entity.Id;
                int tenantId = entity.TenantId;
                int branchId = entity.BranchId;
                var isItemInStore =  await IsItemInStore(storeId, tenantId, branchId);
                if(!isItemInStore)
                {
                    _logger.LogInformation("An Empty store - Id: {storeId} with no items deleted successfully", storeId);
                    _context.Set<Warehouse>().Remove(entity);
                    await _context.SaveChangesAsync(ct);
                    return (true, "Success");
                }
                else
                {
                    return (false, $"Store {storeId} is not empty and cannot be deleted");
                }
            }

            /// <summary>
            /// Only Admins can perform this action
            /// </summary>
            /// <param name="storeId"></param>
            /// <param name="tenaantId"></param>
            /// <param name="branchId"></param>
            /// <returns></returns>
            public async Task AdminDeleteAsync(Guid storeId, int tenaantId, int branchId, CancellationToken ct)
            {
                var storeToDeleteWithItems = await _context.Warehouses
                                                           .Include(x => x.StockItems.Where(x => x.TenantId == tenaantId && x.BranchId == branchId))
                                                           .Include(x => x.WarehouseItems.Where(x => x.StoreId == storeId && x.TenantId == tenaantId && x.BranchId == branchId))
                                                           .Where(x => x.Id == storeId && x.TenantId == tenaantId && x.BranchId == branchId)
                                                           .FirstOrDefaultAsync(cancellationToken: ct);

                if (storeToDeleteWithItems is null)
                {
                    return;
                }
                _context.Remove(storeToDeleteWithItems);
                await _context.SaveChangesAsync(ct);
                _logger.LogInformation("A store with id {storeId} deleted with its items successfully", storeId);
            }

            public async Task<IReadOnlyList<Warehouse>> GetAllStoresByBranchAsync(int tenantId, int branchId, CancellationToken ct)
            {
                return await _context.Warehouses.AsNoTracking().Where(x => x.TenantId == tenantId && x.BranchId == branchId).ToListAsync(cancellationToken: ct);
            }
        }

