using System;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Commands.DeleteSuppliers;

public record DeleteSupplier(Guid SupplierId, int BranchId) : IRequest<DeleteSupplyResponse>;
