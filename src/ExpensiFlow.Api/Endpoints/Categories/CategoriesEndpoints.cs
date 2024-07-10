using System.Net.Mime;
using Ardalis.Result.AspNetCore;
using Asp.Versioning.Builder;
using ExpensiFlow.Api.Endpoints.Categories.Dtos;
using ExpensiFlow.Api.Infrastructure;
using ExpensiFlow.UseCases.Categories.Create;
using ExpensiFlow.UseCases.Categories.Delete;
using ExpensiFlow.UseCases.Categories.Get;
using ExpensiFlow.UseCases.Categories.Search;
using ExpensiFlow.UseCases.Categories.Update;
using Microsoft.AspNetCore.Mvc;

namespace ExpensiFlow.Api.Endpoints.Categories;

public class CategoriesEndpoints : EndpointGroupBase
{
    public override void Map(WebApplication app, ApiVersionSet versionSet)
    {
        var routerBuilder = app.MapGroup(this, "Categories", "v{version:apiVersion}/categories/")
            .RequireAuthorization()
            .WithApiVersionSet(versionSet)
            .HasApiVersion(1.0);

        routerBuilder
            .MapPost(CreateCategory)
            .MapGet(GetCategory, "{id}")
            .MapPut(UpdateCategory, "{id}")
            .MapDelete(DeleteCategory, "{id}")
            .MapGet(SearchCategory);
    }

    private static async Task<IResult> CreateCategory(
        CreateCategoryRequest request,
        ICategoryCreator categoryCreator,
        CancellationToken cancellationToken)
    {
        var result = await categoryCreator.Create(request.ToCommand(), cancellationToken);
        if (result.IsSuccess)
            return Results.Text(result.Value.ToString(), MediaTypeNames.Text.Plain, statusCode: 201);

        return result.ToMinimalApiResult();
    }

    private static async Task<IResult> GetCategory(
        [FromRoute] int id,
        ICategoryGetter categoryGetter,
        CancellationToken cancellationToken)
    {
        var result = await categoryGetter.Get(new GetCategoryQuery(id), cancellationToken);
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToMinimalApiResult();
    }

    private static async Task<IResult> UpdateCategory(
        [FromRoute] int id,
        [FromBody] UpdateCategoryRequest request,
        ICategoryUpdater categoryUpdater,
        CancellationToken cancellationToken)
    {
        var result = await categoryUpdater.Update(request.ToCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Results.Ok();

        return result.ToMinimalApiResult();
    }

    private static async Task<IResult> DeleteCategory(
        [FromRoute] int id,
        ICategoryDeleter categoryDeleter,
        CancellationToken cancellationToken)
    {
        var result = await categoryDeleter.Delete(new DeleteCategoryCommand(id), cancellationToken);
        if (result.IsSuccess)
            return Results.Ok();

        return result.ToMinimalApiResult();
    }

    private static async Task<IResult> SearchCategory(
        [FromQuery] string name,
        [FromQuery] int skip,
        [FromQuery] int take,
        ICategorySearcher categorySearcher,
        CancellationToken cancellationToken)
    {
        var result = await categorySearcher.Search(new SearchCategoryQuery(name, skip, take), cancellationToken);
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToMinimalApiResult();
    }
}