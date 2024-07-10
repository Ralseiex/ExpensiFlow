using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class ExpenseSource : UserEntityBase<int>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}