using Library.Domain.Entities;

namespace Library.Domain.Repositories.InventoryRepo;

public interface IInventoryRepository
{
    Task<Inventory> AddInventoryAsync(Inventory inventory);
    Task<Inventory> GetInventoryByIdAsync(Guid id);
    Task<Inventory> GetInventoryByBookIdAsync(Guid id);
    Task<Inventory> UpdateInventoryAsync(Inventory inventory);
    Task<IEnumerable<Inventory>> GetAllInventoryAsync();
    Task DeleteInventoryAsync(Guid id);
}
