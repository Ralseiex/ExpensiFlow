using Ardalis.Result;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Database;
using ExpensiFlow.UseCases.Categories.Get;
using ExpensiFlow.UseCases.Categories.Search;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Infrastructure.Services.Category;

public class CategoryGetter(ExpensiFlowContext db, IAccountIdAccessor accountIdAccessor) : ICategoryGetter
{
    private readonly ExpensiFlowContext _db = db;
    private readonly AccountId _accountId = accountIdAccessor.GetAccountIdStrict();

    public async Task<Result<CategoryDto>> Get(GetCategoryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var category = await _db.Categories
                .Where(category => category.AccountId == _accountId)
                .Where(category => category.Id == query.Id)
                .Select(entity => new CategoryDto
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    Title = entity.Title
                })
                .SingleOrDefaultAsync(cancellationToken);
            if (category is null)
                return Result.NotFound($"Category {query.Id} not found");

            return category;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}

public class CategorySearcher(ExpensiFlowContext db, IAccountIdAccessor accountIdAccessor) : ICategorySearcher
{
    private readonly ExpensiFlowContext _db = db;
    private readonly AccountId _accountId = accountIdAccessor.GetAccountIdStrict();

    public async Task<Result<IEnumerable<CategoryDto>>> Search(
        SearchCategoryQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var categories = await _db.Categories
                .Where(category => category.AccountId == _accountId)
                .Where(category => EF.Functions.ILike(category.Title, $"%{query.Title}%"))
                .Select(entity => new CategoryDto
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    Title = entity.Title
                })
                .Skip(query.Skip)
                .Take(query.Take)
                .ToArrayAsync(cancellationToken);

            return categories;
        }
        catch (Exception ex)
        {
            return Result.Error(ex.Message);
        }
    }
}