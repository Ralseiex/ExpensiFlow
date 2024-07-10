using Ardalis.Result;

namespace ExpensiFlow.UseCases.Categories.Update;

public interface ICategoryUpdater
{
    Task<Result> Update(UpdateCategoryCommand command, CancellationToken cancellationToken);
}