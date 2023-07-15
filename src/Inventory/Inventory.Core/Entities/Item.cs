using System;

using Inventory.Core.Common;

namespace Inventory.Core.Entities;

public class Item : Entity<Guid>
{
    public int SN { get; set; }  //Serial Number
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int TenantId { get; }
    public int BranchId { get; set; }
    public string UPCNumber { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public bool IsForSale { get; set; }
    public bool IsoftDeleted { get; set; }
    public decimal Price { get; set; }
    public decimal CostPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? WarehouseId { get; set; }
    public int? CategoryID { get; set; }

    public virtual Category? Category { get; set; }

    public DateTime LastModified { get; set; } = DateTime.UtcNow;

    public ICollection<Warehouse> Warehouse { get; set; } = null!;

    //Holds Items that can be found in different Warehouses ie Many to Many Relationship
    //between Items and Warehouses.
    public ICollection<StoreItems> WarehouseItems { get; set; } = null!;

    public Item() { } // Used by EfCore........

    public Item(string name, string description, string unit, Guid warehouseId,
                int tenantId, decimal costPrice, bool isForSale, decimal price, int branchId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Unit = unit;
        WarehouseId = warehouseId;
        TenantId = tenantId;
        CostPrice = costPrice;
        BranchId = branchId;
        UPCNumber = $"{tenantId}{UPCExtension.GenerateUPC(Id)}";
        IsForSale = isForSale;
        Price = isForSale ? price : 0.00M;
        //Category = itemCategory;
    }

    public static Item CreateNewItem(string name, string description, string unit, Guid warehouseId,
                                 int tenantId, decimal costPrice, bool isForSale, decimal price, int branchId)
    {
        return new Item(name, description, unit, warehouseId, tenantId, costPrice, isForSale, price, branchId);
    }
    /*
        In creating an Item, write a service that checks whether the category already exists,
        Retrieve the category Id and Assign the CategoryId to the created Item.
        Else, Create a new Category Instance and Assign assign the CategoryID to the Item

    */

    public void UpdateItems(string name, string unit, decimal costPrice, bool forSale, decimal unitPrice)
    {
        Name = name;
        Unit = unit;
        CostPrice = costPrice;
        IsForSale = forSale;
        Price = forSale ? unitPrice : 0.00M;
        //Category.Name = categoryName;
    }

    // Soft Delete an Item by Setting its IsoftDeleted property to true........
    public void SoftDelete()
    {
        IsoftDeleted = true;
    }
    // Update/Change the Name of an Item......
    // Get the Total Quantity of an Item, if Item is available............. 
}
