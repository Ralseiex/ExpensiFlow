using ExpensiFlow.Domain;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class IncomeConfiguration : UserIdConfiguration<Income, int>
{
    public override void Configure(EntityTypeBuilder<Income> builder)
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
            .WithMany(e => e.Incomes)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}