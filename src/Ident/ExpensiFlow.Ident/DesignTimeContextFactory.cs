using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpensiFlow.Ident;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<IdentContext>
{
    public IdentContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<IdentContext>();
        optionBuilder.UseNpgsql(
            "Host=localhost; Port=5432; Database=expensi-flow-ident; Username=postgres; Password=postgres");
        return new IdentContext(optionBuilder.Options);
    }
}