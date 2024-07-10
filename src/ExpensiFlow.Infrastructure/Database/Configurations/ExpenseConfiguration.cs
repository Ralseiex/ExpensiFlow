using ExpensiFlow.Domain;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class ExpenseConfiguration : UserIdConfiguration<Expense, int>
{
    public override void Configure(EntityTypeBuilder<Expense> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(i => i.Categories)
            .WithMany();
        builder
            .HasMany(i => i.Tags)
            .WithMany();
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired();
    }
}