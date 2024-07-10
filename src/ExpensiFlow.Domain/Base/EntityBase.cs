namespace ExpensiFlow.Domain.Base;

public class EntityBase<TId> where TId : struct, IEquatable<TId>
{
    public TId Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}