using System.Net;
using System.Security.Claims;
using ExpensiFlow.Domain.AccountIdAggregate;
using ExpensiFlow.Infrastructure.Services;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ExpensiFlow.Api.AccountIdAccessor;

public class AccountIdAccessorMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(
        HttpContext httpContext,
        IAccountIdAccessor accountIdAccessor,
        UserService userService)
    {
        if (httpContext.User.Identity is { IsAuthenticated: true })
        {
            var accountIdString = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.NameId)
                                  ?? string.Empty;
            var accountId = AccountId.Parse(accountIdString);

            if (await userService.IsUserExists(accountId))
            {
                accountIdAccessor.AccountId = accountId;
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(httpContext);
        }
    }
}