using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder
            .ToTable("inventory");

        builder
            .Property(i => i.Amount)
            .IsRequired();

        builder
            .HasOne(i => i.Book)
            .WithMany(b => b.inventories)
            .IsRequired();

        builder
            .Property<DateTime>("createdAt")
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder
            .Property<DateTime>("updatedAt")
            .HasDefaultValueSql("getdate()")
            .IsRequired();
    }
}
