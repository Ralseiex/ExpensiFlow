using ExpensiFlow.Ident.Jwt;
using ExpensiFlow.Ident.Login;
using ExpensiFlow.Ident.Register;
using Microsoft.EntityFrameworkCore;

namespace ExpensiFlow.Ident;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
        => services
            .AddScoped<IAccessTokenService, AccessTokenService>()
            .AddScoped<IRegisterService, RegisterService>()
            .AddSingleton<IJwtGenerator, JwtGenerator>();

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ExpensiFlowIdent") ??
                               throw new ConnectionStringIsNotSpecifiedExceptions();
        return services.AddDbContext<IdentContext>(options => options.UseNpgsql(connectionString));
    }
}

public class ConnectionStringIsNotSpecifiedExceptions : Exception;