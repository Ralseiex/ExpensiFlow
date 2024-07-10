using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class Category : UserEntityBase<int>
{
    public required string Title { get; set; }
    public AccountId AccountId { get; set; }
}