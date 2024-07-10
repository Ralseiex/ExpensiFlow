using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class IncomeSource : UserEntityBase<int>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}