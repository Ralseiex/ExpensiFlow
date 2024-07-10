namespace ExpensiFlow.Domain.AccountIdAggregate;

public interface IAccountIdAccessor
{
    public AccountId? AccountId { get; set; }

    public AccountId GetAccountIdStrict() => AccountId ?? throw new AccountIdIsNotSpecifiedException();
}