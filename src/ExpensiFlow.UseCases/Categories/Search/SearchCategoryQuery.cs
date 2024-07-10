namespace ExpensiFlow.UseCases.Categories.Search;

public record SearchCategoryQuery(string Title, int Skip, int Take);