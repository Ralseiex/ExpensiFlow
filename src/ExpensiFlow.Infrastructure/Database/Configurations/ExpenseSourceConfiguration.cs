using ExpensiFlow.Domain;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class ExpenseSourceConfiguration : UserIdConfiguration<ExpenseSource, int>
{
    public override void Configure(EntityTypeBuilder<ExpenseSource> builder)
    {
        base.Configure(builder);
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.ExpenseSources)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}