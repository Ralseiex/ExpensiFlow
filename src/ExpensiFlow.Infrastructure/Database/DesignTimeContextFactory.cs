using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExpensiFlow.Infrastructure.Database;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<ExpensiFlowContext>
{
    public ExpensiFlowContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<ExpensiFlowContext>();
        optionBuilder.UseNpgsql(
            "Host=localhost; Port=5432; Database=expensi-flow; Username=postgres; Password=postgres");
        return new ExpensiFlowContext(optionBuilder.Options);
    }
}