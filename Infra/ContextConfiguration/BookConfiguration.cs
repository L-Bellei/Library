using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .ToTable("books");

        builder
            .Property(b => b.Title)
            .HasMaxLength(60)
            .IsRequired();

        builder
            .Property(b => b.Author)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(b => b.PublishCompany)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .Property(b => b.Subject)
            .HasMaxLength(240)
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
