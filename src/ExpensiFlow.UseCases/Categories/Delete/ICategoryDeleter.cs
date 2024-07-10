using Ardalis.Result;

namespace ExpensiFlow.UseCases.Categories.Delete;

public interface ICategoryDeleter
{
    Task<Result> Delete(DeleteCategoryCommand command, CancellationToken cancellationToken);
}