using ExpensiFlow.Domain.AccountIdAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User = ExpensiFlow.Ident.Models.User;

namespace ExpensiFlow.Ident;

public class IdentContext(DbContextOptions<IdentContext> options) : IdentityUserContext<User, AccountId>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension("uuid-ossp");
        builder.Entity<User>()
            .Property(user => user.Id)
            .HasDefaultValueSql("uuid_generate_v4()")
            .HasConversion(id => id.Value, value => new AccountId(value))
            .IsRequired();
        builder.Entity<User>().HasIndex(x => x.Id).IsUnique();
    }
}