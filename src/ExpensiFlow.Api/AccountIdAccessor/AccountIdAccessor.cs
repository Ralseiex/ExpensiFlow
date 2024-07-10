using ExpensiFlow.Domain.AccountIdAggregate;

namespace ExpensiFlow.Api.AccountIdAccessor;

public class AccountIdAccessor : IAccountIdAccessor
{
    private static readonly AsyncLocal<AccountIdHolder> AccountIdCurrent = new();

    public AccountId? AccountId
    {
        get => AccountIdCurrent.Value?.AccountId;
        set
        {
            var holder = AccountIdCurrent.Value;
            if (holder is not null)
                holder.AccountId = null;

            if (value is not null)
                AccountIdCurrent.Value = new AccountIdHolder { AccountId = value };
        }
    }

    private class AccountIdHolder
    {
        public AccountId? AccountId { get; set; }
    }
}