using System.Text.Json.Serialization;
using Inventory.Core.Common;
using MediatR;

namespace Inventory.Application.Features.Item.Commands.UpdateItem;

public class UpdateItemCommand : IRequest<UpdateItemResponse>
{
    [JsonIgnore]
    public Guid ItemId { get; set; }

    [JsonIgnore]
    public Guid StoreId { get; set; }

    [JsonIgnore]
    public int BranchId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public decimal CostPrice { get; set; }
    public int ItemQuantity { get; set; }
    public bool IsForSale { get; set; }
    public decimal UnitPrice { get; set; }
    public string CategoryName { get; set; } = string.Empty;

}

public class UpdateItemResponse : BaseResponse<string>
{
}