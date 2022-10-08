using Library.Infra.ContextConfiguration;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infra;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Inventory> Inventory { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new UserConfiguration());

        builder.ApplyConfiguration(new BookConfiguration());

        builder.ApplyConfiguration(new InventoryConfiguration());
    }
}
