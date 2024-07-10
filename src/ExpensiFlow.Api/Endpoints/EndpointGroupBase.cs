using Asp.Versioning.Builder;

namespace ExpensiFlow.Api.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app, ApiVersionSet versionSet);
}