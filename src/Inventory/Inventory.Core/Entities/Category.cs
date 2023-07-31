using System;

using Inventory.Core.Common;

namespace Inventory.Core.Entities;

public class Category : Entity<int>
{
    public string Name { get; set; } = string.Empty;
    public int TenantId { get; set; }
    public int BranchId { get; set; }
    public ICollection<Item> ItemCategories { get; set; } = new List<Item>();

    public void UpdateCategory(string categoryName)
    {
        ArgumentException.ThrowIfNullOrEmpty(categoryName);
        Name = categoryName;
    }

    /*
        From the Service in the Application Layer {IsCategoryExistsAsync()},
        Check if the Category exists and asssign the

    */
}
