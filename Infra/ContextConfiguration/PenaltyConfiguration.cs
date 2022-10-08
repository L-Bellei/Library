using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
{
    public void Configure(EntityTypeBuilder<Penalty> builder)
    {
        builder
            .ToTable("penalties");

        builder
            .Property(p => p.PenaltyPrice)
            .IsRequired();

        builder
            .Property(p => p.Settled)
            .IsRequired();

        builder
            .HasOne(i => i.Book)
            .WithMany(b => b.Penalties)
            .IsRequired();

        builder
            .HasOne(i => i.User)
            .WithMany(b => b.Penalties)
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
