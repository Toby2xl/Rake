using System;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Queries.GetSupplier;

public record GetSupplierQuery(Guid SupplierId, int BranchId) : IRequest<SupplierDto>;

public record SupplierDto(Guid SupplierId, string Name, string Email, string PhoneNumber, string Address);
