using System;

namespace Inventory.Application.DbDto;

public class ItemUpdateDto
{
    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public int Quantity { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public int ItemQuantity { get; set; }
    public bool IsForSale { get; set; }
    public decimal UnitPrice { get; set; }
    public int CategoryId { get; set; }
}
