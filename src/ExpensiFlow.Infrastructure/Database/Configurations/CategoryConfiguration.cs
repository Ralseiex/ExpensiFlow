using ExpensiFlow.Domain;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class CategoryConfiguration : UserIdConfiguration<Category, int>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.AccountId)
            .HasConversion(
                id => id.Value,
                value => new AccountId(value));
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Categories)
            .HasForeignKey(e => e.AccountId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired();
    }
}