using Ardalis.Result;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database;
using ExpensiFlow.UseCases.Categories.Create;

namespace ExpensiFlow.Infrastructure.Services.Category;

public class CategoryCreator(ExpensiFlowContext db, IAccountIdAccessor accountIdAccessor) : ICategoryCreator
{
    private readonly ExpensiFlowContext _db = db;
    private readonly AccountId _accountId = accountIdAccessor.GetAccountIdStrict();

    public async Task<Result<int>> Create(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var newCategory = new Domain.Category { Title = command.Title, AccountId = _accountId };
            _db.Categories.Add(newCategory);
            await _db.SaveChangesAsync(cancellationToken);
            return newCategory.Id;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}