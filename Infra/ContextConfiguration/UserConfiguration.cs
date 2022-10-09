using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users");

        builder
            .Property(x => x.UserName)
            .IsRequired();

        builder
            .Property(x => x.Password)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .IsRequired();

        builder
            .Property(x => x.Role)
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
                new User
                {
                    Id = Guid.NewGuid(),
                    UserName = "Admin",
                    Email = "admin@library.com",
                    Password = "admin",
                    Role = "Manager",
                }
            );
    }
}
