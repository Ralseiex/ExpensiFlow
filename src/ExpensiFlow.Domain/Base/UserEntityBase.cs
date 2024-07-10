using ExpensiFlow.Domain.AccountIdAggregate;

namespace ExpensiFlow.Domain.Base;

public class UserEntityBase<TId> : EntityBase<TId> where TId : struct, IEquatable<TId>
{
    public AccountId UserId { get; set; }
    public User? User { get; set; }
}