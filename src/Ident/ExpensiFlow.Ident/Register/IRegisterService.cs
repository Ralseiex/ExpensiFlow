using Ardalis.Result;

namespace ExpensiFlow.Ident.Register;

internal interface IRegisterService
{
    Task<Result> Register(string userName, string password);
}