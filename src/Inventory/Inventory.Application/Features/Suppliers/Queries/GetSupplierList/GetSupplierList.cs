using System;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Queries.GetSupplierList;

public record GetSupplierList(int BranchId) : IRequest<SupplierListResponse>;
