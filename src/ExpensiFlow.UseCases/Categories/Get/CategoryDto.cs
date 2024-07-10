using ExpensiFlow.Domain.AccountIdAggregate;

namespace ExpensiFlow.UseCases.Categories.Get;

public record CategoryDto
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public AccountId AccountId { get; init; }
}