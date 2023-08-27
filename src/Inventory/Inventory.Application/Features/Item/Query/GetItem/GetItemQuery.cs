using System;

using MediatR;

namespace Inventory.Application.Features.Item.Query.GetItem;

public record GetItemQuery(Guid ItemId, Guid StoreId, int BranchId) : IRequest<GetItemResponse>;
