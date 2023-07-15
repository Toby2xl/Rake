using System;

using Inventory.Core.Common;

namespace Inventory.Core.Entities;

public class Supplier : Entity<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public int TenantId { get;}
    public int BranchId { get; set; }
    public bool IsActive { get; set; }
    public bool IsoftDeleted { get; set; }
    public string PhoneNumbers { get; set; } = default!;

    internal Supplier(string name, string email, string address, string phoneNumbers, int tenantId, bool isActive, int branchId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Address = address;
        PhoneNumbers = phoneNumbers;
        TenantId = tenantId;
        IsActive = isActive;
        BranchId = branchId;
    }

    public static Supplier CreateNewSupplier(string name, string email, string address, string phoneNumbers, int tenantId, int branchId)
    {
        return new Supplier(name, email, address, phoneNumbers, tenantId, true, branchId);
    }
    public void UpdateSupplierDetails(string name, string email, string phoneNumbers, string address)
    {
        Name = name;
        Email = email;
        PhoneNumbers = phoneNumbers;
        Address = address;
    }

    //Update Status of a Supplier - Set Active to true or false.......
    public void SetActive()
    {
        if (!IsActive)
            IsActive = true;
    }
    //Change Name of the Supplier....
    //Update Email of the Supplier.....
    // Soft Delete a Supplier by Setting the SoftDelete to True..........
}
