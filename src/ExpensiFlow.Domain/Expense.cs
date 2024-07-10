using ExpensiFlow.Domain.Base;

namespace ExpensiFlow.Domain;

public class Expense : UserEntityBase<int>
{
    public required string Currency { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}