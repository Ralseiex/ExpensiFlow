using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class Tag : UserEntityBase<int>
{
    public required string Title { get; set; }
}