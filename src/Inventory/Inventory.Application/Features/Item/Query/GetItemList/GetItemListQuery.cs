using System;
using MediatR;

namespace Inventory.Application.Features.Item.Query.GetItemList;

public record GetItemListQuery(Guid StoreId, int BranchId, int Cursor, int PageSize) : IRequest<GetItemListResponse>
{

}
