using System;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.DeleteStore;

public record DeleteStoreCommand(Guid StoreId, int BranchId) : IRequest<DeleteResponse>;