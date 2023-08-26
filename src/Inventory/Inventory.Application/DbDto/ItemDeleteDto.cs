using System;

namespace Inventory.Application.DbDto;

public class ItemDeleteDto
{

    public Guid StoreId { get; set; }
    public Guid ItemId { get; set; }
    public int TenantId { get; set; }
    public int BranchId { get; set; }
}
