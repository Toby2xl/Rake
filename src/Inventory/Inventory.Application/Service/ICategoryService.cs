namespace Inventory.Application;

public interface ICategoryService
{
    Task<(bool, int)> DoesCategoryExistAsync(string categoryName, int tenantId, int branchId);
}
