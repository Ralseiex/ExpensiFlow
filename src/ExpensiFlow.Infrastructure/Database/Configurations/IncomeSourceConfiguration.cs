using ExpensiFlow.Domain;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class IncomeSourceConfiguration : UserIdConfiguration<IncomeSource, int>
{
    public override void Configure(EntityTypeBuilder<IncomeSource> builder)
    {
        base.Configure(builder);
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.IncomeSources)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}