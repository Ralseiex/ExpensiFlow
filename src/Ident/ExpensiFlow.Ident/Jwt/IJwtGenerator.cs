using ExpensiFlow.Ident.Models;

namespace ExpensiFlow.Ident.Jwt;

internal interface IJwtGenerator
{
    string Generate(User user, IEnumerable<string> permissions);
}