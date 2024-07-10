using ExpensiFlow.Domain.AccountIdAggregate;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExpensiFlow.Api.AccountIdAccessor;

public static class AccountIdAccessorExtensions
{
    public static IApplicationBuilder UseAccountIdAccessor(this IApplicationBuilder app) 
        => app.UseMiddleware<AccountIdAccessorMiddleware>();

    public static IServiceCollection AddAccountIdAccessor(this IServiceCollection services)
    {
        services.TryAddScoped<IAccountIdAccessor, AccountIdAccessor>();
        return services;
    }
}