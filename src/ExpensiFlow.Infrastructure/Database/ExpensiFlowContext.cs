using ExpensiFlow.Domain;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Infrastructure.Database;

public class ExpensiFlowContext(DbContextOptions<ExpensiFlowContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Expense> Expenses { get; set; } = null!;
    public DbSet<ExpenseSource> ExpenseSources { get; set; } = null!;
    public DbSet<Income> Incomes { get; set; } = null!;
    public DbSet<IncomeSource> IncomeSources { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasPostgresExtension("uuid-ossp");
        builder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssemblyReferenceType).Assembly);
    }
}