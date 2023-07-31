namespace Inventory.Application.Repository;
public interface ITemsRepo
{
    Task<(bool, ItemCreated)> AddStockItemAsync(ItemDto newItemDto);
    Task<(bool, int)> CategoryExistAsync(string name, int tenantId, int branchId);
}
