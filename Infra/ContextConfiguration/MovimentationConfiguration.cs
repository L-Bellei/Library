using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class MovimentationConfiguration : IEntityTypeConfiguration<Movimentation>
{
    public void Configure(EntityTypeBuilder<Movimentation> builder)
    {
        builder
            .ToTable("movimentations");

        builder
            .Property(m => m.MovimentationDate)
            .IsRequired();

        builder
            .HasOne(m => m.User)
            .WithMany(u => u.Movimentations)
            .IsRequired();

        builder
            .HasOne(m => m.Book)
            .WithMany(b => b.Movimentations)
            .IsRequired();
    }
}
