namespace ExpensiFlow.Shared.CorrelationId;

public record CorrelationIdOptions
{
    public string Header { get; init; } = "X-Correlation-ID";
    public bool IncludeInResponse { get; init; } = true;
}