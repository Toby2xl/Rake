namespace Inventory.Application;

public class ItemCreated
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public decimal Quantity { get; set; }
    public bool IsForSale { get; set; }
    public decimal UnitPrice { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string UPCNumber { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
