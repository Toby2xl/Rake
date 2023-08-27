using System;

using Inventory.Core.Common;

namespace Inventory.Application.Features.Item.Query.GetItem;

public class GetItemResponse : BaseResponse<ItemDto>
{

}

public class ItemDto
{
    public Guid ItemId { get; set; }
    public string? Name { get; set; }
    public decimal ItemQuantity { get; set; }
    public string? Units { get; set; }
    public decimal CostPrice { get; set; }
    public decimal TotalCostPrice { get; set; }
    public bool IsForSale { get; set; }
    public decimal Price { get; set; }
    public string? CategoryName { get; set; }
    public string? UPCNumber { get; set; }
}