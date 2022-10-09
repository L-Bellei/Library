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

        builder
            .HasData(
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Lord of the rings - The fellowship of the ring",
                    Author = "J R R Tolkien",
                    PublishCompany = "George Allen & Unwin",
                    Subject = "Frodo and your friends set out on an adventure"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Lord of the rings - The two towers",
                    Author = "J R R Tolkien",
                    PublishCompany = "George Allen & Unwin",
                    Subject = "Frodo and your friends set out on an adventure"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Lord of the rings - The return of the king",
                    Author = "J R R Tolkien",
                    PublishCompany = "George Allen & Unwin",
                    Subject = "Frodo and your friends set out on an adventure"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Prince",
                    Author = "Niccolo Machiavelli",
                    PublishCompany = "Antonio Blado d'Asola",
                    Subject = "It's about Machiavelli vision"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Hobbit: An Unexpected Journey",
                    Author = "J R R Tolkien",
                    PublishCompany = "Mariner Books",
                    Subject = "Bilbo and your friends fighting with a dragon"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Hobbit: The desolation of Smaug",
                    Author = "J R R Tolkien",
                    PublishCompany = "Mariner Books",
                    Subject = "Bilbo and your friends fighting with a dragon"
                },
                new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "The Hobbit: The battle of the five arms",
                    Author = "J R R Tolkien",
                    PublishCompany = "Mariner Books",
                    Subject = "Bilbo and your friends fighting with a dragon"
                }
            );
    }
}
