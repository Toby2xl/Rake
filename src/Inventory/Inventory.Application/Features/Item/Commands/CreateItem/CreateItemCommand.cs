using Inventory.Core.ValueObject;

using MediatR;

namespace Inventory.Application;

public class CreateItemCommand : IRequest<CreateItemResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public QuantityDetails QtyDetails { get; set; } = null!;
    public decimal CostPrice { get; set; }
    public bool IsForSale { get; set; }
    public decimal UnitPrice { get; set; }
    public Guid StoreId { get; set; }
    public int BranchId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}
