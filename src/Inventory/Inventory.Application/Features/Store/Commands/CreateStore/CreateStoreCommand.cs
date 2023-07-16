using System;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.CreateStore;

public record CreateStoreCommand(string Name,
                                string Description,
                                string Code, string Address, int BranchId) : IRequest<CreateStoreResponse>;
