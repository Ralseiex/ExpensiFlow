using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class User : EntityBase<AccountId>
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
    public ICollection<Income> Incomes { get; set; } = [];
    public ICollection<IncomeSource> IncomeSources { get; set; } = [];
    public ICollection<Expense> Expenses { get; set; } = [];
    public ICollection<ExpenseSource> ExpenseSources { get; set; } = [];
}