using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ExpensiFlow.Shared.CorrelationId;

public class CorrelationIdMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
{
    private readonly RequestDelegate _next = next;
    private readonly CorrelationIdOptions _options = options.Value;

    public async Task InvokeAsync(HttpContext context)
    {
        var hasCorrelationIdHeader = context.Request.Headers.TryGetValue(_options.Header, out var cid) &&
                                     !string.IsNullOrEmpty(cid);
        var correlationId = hasCorrelationIdHeader
            ? cid.First() ?? Guid.NewGuid().ToString()
            : Guid.NewGuid().ToString();
        context.TraceIdentifier = correlationId;

        if (_options.IncludeInResponse)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[_options.Header] = new[] { context.TraceIdentifier };
                return Task.CompletedTask;
            });
        }

        await _next(context);
    }
}