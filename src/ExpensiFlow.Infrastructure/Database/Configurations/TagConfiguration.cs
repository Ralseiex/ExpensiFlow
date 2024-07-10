using ExpensiFlow.Domain;
using ExpensiFlow.Infrastructure.Database.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExpensiFlow.Infrastructure.Database.Configurations;

internal class TagConfiguration : UserIdConfiguration<Tag, int>
{
    public override void Configure(EntityTypeBuilder<Tag> builder)
    {
        base.Configure(builder);
        builder
            .HasOne(e => e.User)
            .WithMany(e => e.Tags)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
    }
}