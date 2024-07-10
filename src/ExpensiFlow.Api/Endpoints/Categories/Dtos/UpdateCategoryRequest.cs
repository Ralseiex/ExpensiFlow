using ExpensiFlow.UseCases.Categories.Update;

namespace ExpensiFlow.Api.Endpoints.Categories.Dtos;

public class UpdateCategoryRequest
{
    public required string Title { get; set; }

    public UpdateCategoryCommand ToCommand(int id) => new UpdateCategoryCommand(id, Title);
}