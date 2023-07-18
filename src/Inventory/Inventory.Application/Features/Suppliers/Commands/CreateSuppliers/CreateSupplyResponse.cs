using Inventory.Core.Common;

namespace Inventory.Application.Features.Suppliers.Commands.CreateSuppliers;

public class CreateSupplyResponse : BaseResponse<CreateSupplierDto>
{
}

public record CreateSupplierDto(Guid Id, string Name, string Email, string Address, string PhoneNumber, int BranchId);
