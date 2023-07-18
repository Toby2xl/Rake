using System;
using System.Text.Json.Serialization;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Commands.UpdateSuppliers;

public record UpdateSupplier(string Name, string Email, string PhoneNumbers, string Address): IRequest<UpdateSupplyResponse>
{
    [JsonIgnore]
    public Guid SupplierId { get; init; }

    [JsonIgnore]
    public int BranchId { get; init; }
}
