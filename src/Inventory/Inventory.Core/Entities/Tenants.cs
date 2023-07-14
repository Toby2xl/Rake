using System;

namespace Inventory.Core.Entities;

public class Tenants
{
    public int Id { get; set; }
    public int Sn { get; set; }
    public string TenantName { get; set; } = string.Empty;
    public string SchoolName { get; set; } = string.Empty;
    public DateTime? DateOnboarded { get; set; }
    public int? NoOfSubscribedUsers { get; set; }
    public DateTime? SubscriptionDate { get; set; }
    public DateTime? SubscriptionExpiryDate { get; set; }
    public decimal? AmountPaid { get; set; }
    public string ContactPhoneNumber { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    public int? Status { get; set; }
}
