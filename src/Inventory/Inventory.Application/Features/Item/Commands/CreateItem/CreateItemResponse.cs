using Inventory.Core.Common;

namespace Inventory.Application;

public class CreateItemResponse : BaseResponse<CreateItemDto>
{

}

public record CreateItemDto(Guid Id, string Name, decimal Quantity,
                            decimal Price, bool IsForSale,
                            string CategoryName, string UPCNumber, string Message);


/*
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        //public JsonDocument QuantityDetails { get; set; }
        public decimal CostPrice { get; set; }
        public bool IsForSale { get; set; }
        public decimal Price { get; set; }
        public string  CategoryName { get; set; }
        public string UPCNumber { get; set; }
        public string Message { get; set; }

*/