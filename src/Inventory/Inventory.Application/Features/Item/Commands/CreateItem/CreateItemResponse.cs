using Inventory.Core.Common;

namespace Inventory.Application;

public class CreateItemResponse : BaseResponse<CreateItemDto>
{

}

public record CreateItemDto(Guid Id, string Name, decimal CostPrice, decimal Quantity,
                            decimal Price, bool IsForSale,
                            string CategoryName, string UPCNumber, string Message);
