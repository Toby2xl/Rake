using System;
using System.Text.Json.Serialization;

using MediatR;

namespace Inventory.Application.Features.Store.Commands.UpdateStore
{
    public record UpdateStoreCommand(string Name, string Description, string Code, string Address) : IRequest<UpdateStoreResponse>
    {
       [JsonIgnore]
       public Guid StoreId {get; set;}

        [JsonIgnore]
        public int BranchId { get; set; }
        // public string Name { get; set; } = string.Empty;
        // public string Description { get; set; } = string.Empty;
        // public string Code { get; set; } = string.Empty;
        // public string Address { get; set; } = string.Empty;
    }
}
