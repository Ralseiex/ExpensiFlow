using Ardalis.Result;

namespace ExpensiFlow.UseCases.Categories.Create;

public interface ICategoryCreator
{
    Task<Result<int>> Create(CreateCategoryCommand category, CancellationToken cancellationToken);
}