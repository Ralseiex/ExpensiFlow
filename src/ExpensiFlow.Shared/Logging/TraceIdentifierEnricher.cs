using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace ExpensiFlow.Shared.Logging;

public class TraceIdentifierEnricher(IHttpContextAccessor contextAccessor) : ILogEventEnricher
{
    private const string TraceIdentifierPropertyName = "TraceIdentifier";
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var property = propertyFactory.CreateProperty(
            TraceIdentifierPropertyName,
            _contextAccessor.HttpContext?.TraceIdentifier ?? "-");
        logEvent.AddOrUpdateProperty(property);
    }
}