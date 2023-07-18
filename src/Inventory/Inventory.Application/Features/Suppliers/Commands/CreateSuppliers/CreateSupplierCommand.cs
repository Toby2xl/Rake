using System;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Commands.CreateSuppliers;

public record CreateSupplierCommand(string Name,
                                    string Email,
                                    string Address,
                                    string PhoneNumber,
                                    int BranchId): IRequest<CreateSupplyResponse>;