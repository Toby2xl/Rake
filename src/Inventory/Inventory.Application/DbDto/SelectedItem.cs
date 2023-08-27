using System;

namespace Inventory.Application.DbDto;

public class SelectedItem
{
    public Guid ItemId { get; set; }
    public int Sn { get; set; }
    public required string Name { get; set; }
    public decimal ItemQuantity { get; set; }
    public string? Description { get; set; }
    public required string Units { get; set; }
    public decimal CostPrice { get; set; }
    public bool IsForSale { get; set; }
    public decimal Price { get; set; }
    public string? CategoryName { get; set; }
    public string? UPCNumber { get; set; }
}
