using System;

using MediatR;

namespace Inventory.Application.Features.Store.Queries.GetStoreList;

public record GetStoreListQuery(int BranchId) : IRequest<List<StoreListVm>>;

public record StoreListVm(Guid StoreId, string Name, string Description, string Code, string Address, int BranchId);
