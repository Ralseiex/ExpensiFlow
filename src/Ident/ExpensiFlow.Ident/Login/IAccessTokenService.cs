using Ardalis.Result;

namespace ExpensiFlow.Ident.Login;

internal interface IAccessTokenService
{
    Task<Result<string>> GetToken(string userName, string password);
    Task<bool> ValidateToken(string token);
}