using Inventory.Application;
using Inventory.Application.DbDto;
using Inventory.Application.Repository;
using Inventory.Core.Entities;
using Inventory.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Inventory.Infrastructure;

public class ItemRepo : ITemsRepo
{
    private readonly InventoryDbContext _context;
    private readonly ILogger<ItemRepo> _logger;
    public ItemRepo(InventoryDbContext context, ILogger<ItemRepo> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(bool, ItemCreated)> AddStockItemAsync(ItemDto newItemDto)
    {
        var itemName = newItemDto.NewItem.Name.ToLower();
        int tenantId = newItemDto.NewItem.TenantId;
        int branchId = newItemDto.NewItem.BranchId;
        var storeId = newItemDto.NewItem.WarehouseId;
        try
        {
            if(!DoesStoreExist(storeId))
            {
                return (false, new ItemCreated
                {
                    Message = "There's no such Store name",
                });
            }
            if (await _context.Items.AnyAsync(x => x.WarehouseId == storeId && x.Name.ToLower() == itemName && x.TenantId == tenantId && x.BranchId == branchId))
            {
                return (false, new ItemCreated
                {
                    Message = $"{itemName} - Item already exists.",
                });
            }
            //construct the join table
            var storeItem = CreateStoreItems(newItemDto, tenantId, branchId);

            await _context.AddRangeAsync(newItemDto.NewItem, storeItem);
            await _context.SaveChangesAsync();

            return (true, new ItemCreated
            {
                Id = newItemDto.NewItem.Id,
                Name = newItemDto.NewItem.Name,
                CostPrice = newItemDto.NewItem.CostPrice,
                Quantity = storeItem.Instock,
                IsForSale = newItemDto.NewItem.IsForSale,
                UnitPrice = newItemDto.NewItem.Price,
                UPCNumber = newItemDto.NewItem.UPCNumber,
                Message = $"{newItemDto.NewItem.Name} Added succesfully",
            });
        }
        catch (Exception e)
        {
            _logger.LogCritical("Item Add Error:=> {e}", e.Message);
            return (false, default!);
        }
    }

    private static StoreItems CreateStoreItems(ItemDto newItemDto, int tenantId, int branchId)
    {
        return new StoreItems
        {
            StoreId = newItemDto.NewItem.WarehouseId!.Value,
            ItemId = newItemDto.NewItem.Id,
            TenantId = tenantId,
            BranchId = branchId,
            Instock = newItemDto.Quantity,
            UPCNumber = newItemDto.NewItem.UPCNumber,
            QuantityDetail = newItemDto.QuantityDetails

        };
    }

    private bool DoesStoreExist(Guid? storeId)
    {
        return _context.Warehouses.Any(x => x.Id == storeId);
    }

    public async Task<(bool, int)> CategoryExistAsync(string name, int tenantId, int branchId)
    {
        string categoryName = name.ToLower();
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name.ToLower() == categoryName && c.TenantId == tenantId && c.BranchId == branchId);
        if(category is null)
        {
            return (false, int.MinValue);
        }
        var categoryId = category.Id;
        return (true, categoryId);
    }
    public async Task<Item> GetItemByIdAsync(Guid itemId, int tenantId, int branchId, CancellationToken ct)
    {
        var item = await _context.Items.Where(x => x.Id == itemId &&
                                             x.TenantId == tenantId && x.BranchId == branchId).SingleOrDefaultAsync(ct);
        return item is not null ? item : default!;
    }

    public async Task<IReadOnlyList<Item>> GetAllStockItemByStoreAsync(Guid storeId, int tenantId, int branchId, CancellationToken ct)
    {
        var items = await _context.Items
                                    .Where(x => x.WarehouseId == storeId
                                        && x.TenantId == tenantId && x.BranchId == branchId).ToListAsync(ct);
        return items;
    }

    public async Task DeleteAsync(ItemDeleteDto entity, CancellationToken ct)
    {
        int tenantId = entity.TenantId;
        int branchId = entity.BranchId;
        Guid storeId = entity.StoreId;
        Guid itemId = entity.ItemId;
        // WarehouseItems == StoreItems - the join table between warehouse and Items.
        var itemToDelete = await _context.Items
                                               .Include(x => x.WarehouseItems
                                                        .Where(x => x.StoreId == storeId &&
                                                                    x.ItemId == itemId &&
                                                                    x.TenantId == tenantId && x.BranchId == branchId))
                                               .Where(x => x.Id == itemId && x.TenantId == tenantId && x.BranchId == branchId).FirstOrDefaultAsync(ct);

        _context.Remove(itemToDelete!);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateItemAsync(ItemUpdateDto entity, int tenantId, int branchId, CancellationToken ct)
    {
        var itemId = entity.ItemId;
        var storeId = entity.StoreId;
        int categoryName = entity.CategoryId;
        var itemToUpdate = await _context.Items.Where(x => x.Id == itemId &&
                                                      x.TenantId == tenantId && x.BranchId == branchId)
                                                      .FirstOrDefaultAsync(ct);

        itemToUpdate!.UpdateItems(entity.Name, entity.Unit,
                                 entity.CostPrice, entity.IsForSale,
                                 entity.UnitPrice, entity.CategoryId);

        var storeItems = await _context.StoreItems
                                                .Include(x => x.Item)
                                                .Where(x => x.StoreId == storeId && x.ItemId == itemId
                                                     && x.TenantId == tenantId && x.BranchId == branchId).FirstOrDefaultAsync();

        storeItems!.Instock = entity.Quantity;
        storeItems.Item = itemToUpdate;
        _context.Update(storeItems);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<SelectedItem?> GetSelectedItemByIdAsync(Guid itemId, Guid storeId, int tenantId, int branchId)
    {
        var item = await _context.Items.Where(x => x.Id == itemId &&
                                                x.TenantId == tenantId && x.BranchId == branchId)
                                                .Include(x => x.Category)
                                                .Include(x => x.WarehouseItems.Where(x => x.TenantId == tenantId && x.BranchId == branchId))
                                                .Select(c => new SelectedItem
                                                {
                                                    ItemId = c.Id,
                                                    Name = c.Name,
                                                    ItemQuantity = c.WarehouseItems.Where(x => x.StoreId == storeId && x.ItemId == c.Id)
                                                                               .Select(x => x.Instock).SingleOrDefault(),
                                                    Units = c.Unit,
                                                    IsForSale = c.IsForSale,
                                                    CostPrice = c.CostPrice,
                                                    Price = c.Price,
                                                    CategoryName = c.Category!.Name,
                                                    UPCNumber = c.UPCNumber,
                                                })
                                                .SingleOrDefaultAsync();
        return item is not null ? item : default;
    }

    public async Task<(int, IReadOnlyList<SelectedItem>)> GetAllItemByStoreAsync(Guid storeId, int tenantId, int branchId, int cursor, int pageSize, CancellationToken ct)
    {
        try
        {
            //TODO: Add pagination here, if all is well..... KeySet Pagination
            var ItemsList = await _context.Items.AsNoTracking()
                                        .Where(x => x.WarehouseId == storeId
                                            && x.TenantId == tenantId && x.BranchId == branchId && x.SN >= cursor)
                                            .Include(x => x.Category)
                                            .Include(x => x.WarehouseItems.Where(x => x.TenantId == tenantId && x.BranchId == branchId))
                                            //.Where(x => x.SN >= cursor)
                                            .OrderBy(x => x.SN)
                                            .Take(pageSize + 1)
                                            .Select(c => new SelectedItem
                                            {
                                                ItemId = c.Id,
                                                Sn = c.SN,
                                                Name = c.Name,
                                                Description = c.Description,
                                                ItemQuantity = c.WarehouseItems.Where(x => x.StoreId == storeId && x.ItemId == c.Id).Select(x => x.Instock).SingleOrDefault(),
                                                CostPrice = c.CostPrice,
                                                Units = c.Unit,
                                                IsForSale = c.IsForSale,
                                                Price = c.Price,
                                                CategoryName = c.Category!.Name,
                                                UPCNumber = c.UPCNumber,
                                            })
                                            .ToListAsync();
            int nextCursor = ItemsList[^1].Sn;
            // int prevCursor = ItemsList[0].Sn;
            // var cat = ItemsList.Last().Sn;
            return (nextCursor, ItemsList.Take(pageSize).ToList());

        }
        catch (System.Exception e)
        {
            _logger.LogCritical($"Item Add Error:==> {e}");
            // throw;
            //return (0, Enumerable.Empty<SelectedItem>());
            return (0, new List<SelectedItem>());
        }
    }
}
