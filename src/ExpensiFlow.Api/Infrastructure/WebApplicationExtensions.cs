using System.Reflection;
using Asp.Versioning.Builder;
using ExpensiFlow.Api.Endpoints;

namespace ExpensiFlow.Api.Infrastructure;

public static class WebApplicationExtensions
{
    public static RouteGroupBuilder MapGroup(
        this WebApplication app,
        EndpointGroupBase group,
        string? tag = null,
        string? prefix = null)
    {
        tag ??= group.GetType().Name;
        prefix ??= string.Empty;

        return app
            .MapGroup($"api/{prefix}")
            .WithTags(tag)
            .WithOpenApi();
    }

    public static WebApplication MapEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var endpoints = assembly.GetExportedTypes()
            .Where(type => type.IsSubclassOf(typeof(EndpointGroupBase)));

        foreach (var endpoint in endpoints)
        {
            if (Activator.CreateInstance(endpoint) is EndpointGroupBase instance)
                instance.Map(app, versionSet);
        }

        return app;
    }
}