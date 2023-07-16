using System;

using Inventory.Core.Common;

namespace Inventory.Application.Features.Store.Commands.CreateStore;

public class CreateStoreResponse : BaseResponse<CreateStoreDto>
{
}

public record CreateStoreDto(Guid StoreId, string Name, string Description, string Code, string Address);
