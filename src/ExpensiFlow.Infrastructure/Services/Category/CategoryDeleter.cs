using Ardalis.Result;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database;
using ExpensiFlow.UseCases.Categories.Delete;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Infrastructure.Services.Category;

public class CategoryDeleter(ExpensiFlowContext db, IAccountIdAccessor accountIdAccessor) : ICategoryDeleter
{
    private readonly ExpensiFlowContext _db = db;
    private readonly AccountId _accountId = accountIdAccessor.GetAccountIdStrict();

    public async Task<Result> Delete(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _db.Categories
                .Where(category => category.AccountId == _accountId)
                .Where(category => category.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);
            if (category is null)
                return Result.NotFound($"Category {command.Id} not found");

            _db.Remove(category);
            await _db.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}