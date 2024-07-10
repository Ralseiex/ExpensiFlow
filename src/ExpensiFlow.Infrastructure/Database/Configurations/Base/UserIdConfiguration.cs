using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Domain.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations.Base;

internal abstract class UserIdConfiguration<TEntity, TId> : BaseConfiguration<TEntity, TId>
    where TEntity : UserEntityBase<TId>
    where TId : struct, IEquatable<TId>
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.UserId)
            .HasConversion(
                id => id.Value,
                value => new AccountId(value));
    }
}