using Ardalis.Result;
using ExpensiFlow.Ident.Jwt;
using ExpensiFlow.Ident.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpensiFlow.Ident.Login;

internal class AccessTokenService(IJwtGenerator jwtGenerator, UserManager<User> userManager) : IAccessTokenService
{
    private readonly IJwtGenerator _jwtGenerator = jwtGenerator;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<Result<string>> GetToken(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            return Result.NotFound("User not found");
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
        if (isPasswordValid == false)
            return Result.Error("Invalid password");

        return _jwtGenerator.Generate(user, Array.Empty<string>());
    }

    public Task<bool> ValidateToken(string token) => Task.FromResult(false);
}