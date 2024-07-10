using Microsoft.AspNetCore.Builder;

namespace ExpensiFlow.Shared.CorrelationId;

public static class CorrelationIdExtensions
{
    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app) 
        => app.UseMiddleware<CorrelationIdMiddleware>();
}