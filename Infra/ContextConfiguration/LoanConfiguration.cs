using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infra.ContextConfiguration;

public class LoanConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder
            .Property(l => l.Returned)
            .IsRequired();

        builder
            .Property(l => l.DevolutionDate);

        builder
            .Property(l => l.DeadlineDate)
            .IsRequired();

        builder
            .Property(l => l.LoanDate)
            .IsRequired();

        builder
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .IsRequired();

        builder
            .HasOne(l => l.User)
            .WithMany(b => b.Loans)
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
