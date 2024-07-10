namespace ExpensiFlow.Domain.AccountIdAggregate;

[StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson)]
public partial struct AccountId
{
    public static AccountId? FromGuid(Guid? guid) => guid is null ? null : new AccountId(guid.Value);
    public static AccountId FromGuid(Guid guid) => new AccountId(guid);
    public static AccountId Parse(string guid) => new AccountId(Guid.Parse(guid));
}