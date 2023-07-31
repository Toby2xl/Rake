namespace Inventory.Application.Repository;
public interface ITemsRepo
{
    Task<(bool, ItemCreated)> AddStockItemAsync(ItemDto newItem);
}
