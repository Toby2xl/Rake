using Inventory.Core.ValueObject;

namespace Inventory.Core.Entities;

/// <summary>
/// The Join Table in Many to Many Relationships between Warehouses and Items.....
/// </summary>
public class StoreItems
{
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public int TenantId { get; set; }
    public int BranchId { get; set; }
    public decimal Instock { get; set; } // Quantity of Items
    public string UPCNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime LastModified { get; set; } = DateTime.UtcNow;
    public QuantityDetails QuantityDetail { get; set; } = null!;
    public virtual Item Item { get; set; } = null!; //assign to new instances if EfCore complains
    public virtual Warehouse Warehouse { get; set; } = null!;
}