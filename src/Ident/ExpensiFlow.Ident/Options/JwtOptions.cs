﻿namespace ExpensiFlow.Ident.Options;

public record JwtOptions
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SigningKey { get; init; }
    public required string Subject { get; init; }
    public int ExpirationSeconds { get; init; }
}