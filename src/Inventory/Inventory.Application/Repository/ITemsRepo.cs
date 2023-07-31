namespace Inventory.Application.Repository;
public interface ITemsRepo
{
    Task<(bool, ItemCreated)> AddStockItemAsync(ItemDto newItem);
    Task<(bool, int)> CategoryExistAsync(string name, int tenantId, int branchId);
}
