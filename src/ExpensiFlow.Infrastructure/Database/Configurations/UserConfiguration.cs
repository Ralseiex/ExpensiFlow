using ExpensiFlow.Domain;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class UserConfiguration : BaseConfiguration<User, AccountId>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Id)
            .HasConversion(
                id => id.Value,
                value => new AccountId(value));
    }
}