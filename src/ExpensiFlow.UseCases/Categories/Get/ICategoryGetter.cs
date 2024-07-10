using Ardalis.Result;

namespace ExpensiFlow.UseCases.Categories.Get;

public interface ICategoryGetter
{
    Task<Result<CategoryDto>> Get(GetCategoryQuery query, CancellationToken cancellationToken);
}