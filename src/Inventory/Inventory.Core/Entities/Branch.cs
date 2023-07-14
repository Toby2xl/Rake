using System;

namespace Inventory.Core.Entities;

public class Branch
{
    public int BranchId { get; set; }
    public string BranchName { get; set; } = string.Empty;
    public int TenantId { get; set; }
}
