using Inventory.Core.Common;

namespace Inventory.Application.Features.Suppliers.Queries.GetSupplierList;

public class SupplierListResponse : BaseResponse<IEnumerable<SupplierListDto>>
{
}

public record SupplierListDto(Guid SupplierId, string Name, string Email, string Address, string PhoneNumber, int BranchId);