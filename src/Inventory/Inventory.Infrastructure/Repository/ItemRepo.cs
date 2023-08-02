﻿using Inventory.Application;
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
}
