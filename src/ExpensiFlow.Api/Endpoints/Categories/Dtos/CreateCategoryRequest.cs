using ExpensiFlow.UseCases.Categories.Create;

namespace ExpensiFlow.Api.Endpoints.Categories.Dtos;

public class CreateCategoryRequest
{
    public required string Title { get; set; }

    public CreateCategoryCommand ToCommand() => new CreateCategoryCommand(Title);
}