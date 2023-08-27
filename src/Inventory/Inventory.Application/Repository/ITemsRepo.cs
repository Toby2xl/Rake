using Inventory.Application.DbDto;
using Inventory.Core.Entities;

namespace Inventory.Application.Repository;
public interface ITemsRepo
{
    Task<(bool, ItemCreated)> AddStockItemAsync(ItemDto newItemDto);
    Task<(bool, int)> CategoryExistAsync(string name, int tenantId, int branchId);
    Task<Item> GetItemByIdAsync(Guid itemId, int tenantId, int branchId, CancellationToken ct);
    Task<IReadOnlyList<Item>> GetAllStockItemByStoreAsync(Guid storeId, int tenantId, int branchId, CancellationToken ct);
    Task DeleteAsync(ItemDeleteDto entity, CancellationToken ct);

    Task UpdateItemAsync(ItemUpdateDto entity, int tenantId, int branchId, CancellationToken ct);

    Task<SelectedItem?> GetSelectedItemByIdAsync(Guid itemId, Guid storeId, int tenantId, int branchId);
    Task<(int, IReadOnlyList<SelectedItem>)> GetAllItemByStoreAsync(Guid storeId, int tenantId,
                                                                    int branchId, int cursor, int pageSize,
                                                                     CancellationToken ct);
}
