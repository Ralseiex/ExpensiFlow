using Ardalis.Result;
using ExpensiFlow.UseCases.Categories.Get;

namespace ExpensiFlow.UseCases.Categories.Search;

public interface ICategorySearcher
{
    Task<Result<IEnumerable<CategoryDto>>> Search(SearchCategoryQuery query, CancellationToken cancellationToken);
}