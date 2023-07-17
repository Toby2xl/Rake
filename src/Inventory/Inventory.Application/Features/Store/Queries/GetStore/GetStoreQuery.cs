using System;

using MediatR;

namespace Inventory.Application.Features.Store.Queries.GetStore;

public record GetStoreQuery(Guid StoreId, int BranchId) : IRequest<StoreDto>;

public record StoreDto(string Name, string Description, string Code, string Address);
