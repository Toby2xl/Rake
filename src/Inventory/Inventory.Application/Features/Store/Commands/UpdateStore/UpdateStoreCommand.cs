using System;
using System.Text.Json.Serialization;

namespace Inventory.Application.Features.Store.Commands.UpdateStore
{
    public class UpdateStoreCommand
    {
        [JsonIgnore]
       public Guid StoreId {get; set;}

        [JsonIgnore]
        public int BranchId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
