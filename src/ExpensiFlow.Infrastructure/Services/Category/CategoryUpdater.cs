using Ardalis.Result;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database;
using ExpensiFlow.UseCases.Categories.Update;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Infrastructure.Services.Category;

public class CategoryUpdater(ExpensiFlowContext db, IAccountIdAccessor accountIdAccessor) : ICategoryUpdater
{
    private readonly ExpensiFlowContext _db = db;
    private readonly AccountId _accountId = accountIdAccessor.GetAccountIdStrict();

    public async Task<Result> Update(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _db.Categories
                .Where(category => category.AccountId == _accountId)
                .Where(category => category.Id == command.Id)
                .SingleOrDefaultAsync(cancellationToken);
            if (category is null)
                return Result.NotFound($"Category {command.Id} not found");

            category.Title = command.NewTitle;

            await _db.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}