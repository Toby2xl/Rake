using System;

using Inventory.Core.Common;

namespace Inventory.Core.Entities;

public class Customer : Entity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public int TenantId { get; set; }
    public int BranchId { get; set; }

    public Guid? StockSalesId { get; set; }

    public Customer()
    {
        Id = Guid.NewGuid();
    }
}
