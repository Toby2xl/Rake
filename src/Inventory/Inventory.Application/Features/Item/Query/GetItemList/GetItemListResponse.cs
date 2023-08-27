using System;
using Inventory.Core.Common;

namespace Inventory.Application.Features.Item.Query.GetItemList;

public class GetItemListResponse : BaseResponse<IEnumerable<ItemsListVm>>
{
    public int? NextCursor { get; set; }
}

public class ItemsListVm
{
    public Guid ItemId { get; set; }
    public int Sn { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal ItemQuantity { get; set; }
    public string Unit { get; set; } = string.Empty;
    public string? UPCNumber { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public bool IsForSale { get; set; }
    public decimal UnitPrice { get; set; }

    public ItemsListVm(Guid id, int sn, string name, decimal quantity, string unit, string upcNumber, string categoryName, decimal costPrice, bool forSale, decimal unitPrice)
    {
        ItemId = id;
        Sn = sn;
        ItemName = name;
        ItemQuantity = quantity;
        Unit = unit;
        UPCNumber = upcNumber;
        CategoryName = categoryName;
        CostPrice = costPrice;
        IsForSale = forSale;
        UnitPrice = unitPrice;
    }

}
