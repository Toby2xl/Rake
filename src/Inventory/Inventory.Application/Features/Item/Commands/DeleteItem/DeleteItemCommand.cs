using System;

using Inventory.Core.Common;

using MediatR;

namespace Inventory.Application.Features.Item.Commands.DeleteItem;

public record DeleteItemCommand(Guid ItemId, Guid StoreId, int BranchId) : IRequest<DeleteResponse>;

public class DeleteResponse : BaseResponse<string>
{
}