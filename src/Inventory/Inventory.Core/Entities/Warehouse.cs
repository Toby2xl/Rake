using System;
using Inventory.Core.Common;

namespace Inventory.Core.Entities;

public class Warehouse : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;
    public int TenantId { get; }
    public int BranchId { get; }
    public DateTime CreatedAt { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;


    private readonly List<Item> _storeItems = new();
    public IReadOnlyCollection<Item> StockItems => _storeItems.AsReadOnly();
    public ICollection<StoreItems> WarehouseItems { get; set; } = new List<StoreItems>();


    private Warehouse() {}
    public Warehouse(string name, string description, DateTime createdAt, int tenantId, string code, string address, int branchId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = createdAt;
        TenantId = tenantId;
        Code = code;
        Address = address;
        BranchId = branchId;

        _storeItems = new List<Item>();
        //WarehouseItems = new List<StoreItems>();
    }

    public static Warehouse CreateNewStore(string name, string description, string code, string address, DateTime createdAt, int teneatId, int branchId = 1)
    {
        return new Warehouse(name, description, createdAt, teneatId, code, address, branchId);
    }

    public void UpdateStoreDetails(string name, string descriptioin, string code, string address)
    {
        Name = name;
        Description = descriptioin;
        Code = code;
        Address = address;
    }

    /*Create a function that accepts a list of Items and append it to the Warehouse
      Bulk Item Upload....
    */
}
