using ExpensiFlow.Domain.AccountIdAggregate;

namespace ExpensiFlow.Ident.Events;

public record AccountCreated(AccountId Id, string Name, string Email);