using Library.Domain.Entities;
using Library.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.Domain.Repositories.InventoryRepo;

public class InventoryRepository : IInventoryRepository
{
    private readonly ApplicationDbContext db;

    public InventoryRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<Inventory> AddInventoryAsync(Inventory inventory)
    {
        await db.Inventory.AddAsync(inventory);
        await db.SaveChangesAsync();

        return inventory;
    }

    public async Task DeleteInventoryAsync(Guid id)
    {
        Inventory? inventory = db.Inventory.Where(i => i.Id == id).FirstOrDefault();

        if (inventory == null)
            throw new Exception("Register not found");

        db.Inventory.Remove(inventory);
        await db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
    {
        IEnumerable<Inventory> inventory = await db.Inventory.ToListAsync();

        if (inventory == null)
            throw new Exception("Not registered yet");
    
        return inventory;
    }

    public async Task<Inventory> GetInventoryByIdAsync(Guid id)
    {
        Inventory? inventoryRegistry = await db.Inventory.FirstOrDefaultAsync(i => i.Id == id);

        if (inventoryRegistry == null)
            throw new Exception("Register not found");

        return inventoryRegistry;
    }

    public async Task<Inventory> GetInventoryByBookIdAsync(Guid id)
    {
        Inventory? inventoryRegistry = await db.Inventory.FirstOrDefaultAsync(i => i.BookId == id);

        if (inventoryRegistry == null)
            throw new Exception("Register not found");

        return inventoryRegistry;
    }

    public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
    {
        EntityEntry<Inventory> _inventory = db.Entry(inventory);

        _inventory.Property<DateTime>("updatedAt").CurrentValue = DateTime.Now;
        _inventory.State = EntityState.Modified;

        db.Update(inventory);
        await db.SaveChangesAsync();

        return inventory;
    }
}
