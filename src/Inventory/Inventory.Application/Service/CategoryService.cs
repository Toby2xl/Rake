using System;

using Inventory.Application.Repository;

namespace Inventory.Application.Service;

public class CategoryService : ICategoryService
{
    private readonly ITemsRepo _itemRepo;
    public CategoryService(ITemsRepo itemRepo)
    {
        _itemRepo = itemRepo;
    }
    public async Task<(bool, int)> DoesCategoryExistAsync(string categoryName, int tenantId, int branchId)
    {
        ArgumentException.ThrowIfNullOrEmpty(categoryName);
        var (success, categoryId) = await _itemRepo.CategoryExistAsync(categoryName, tenantId, branchId);
        return (success, categoryId);
    }
}
