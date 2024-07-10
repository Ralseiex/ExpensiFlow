using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensiFlow.Shared.Configurations;

public static class ConfigurationsExtensions
{
    public static IServiceCollection RegisterOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null) where T : class
        => services.Configure<T>(configuration.GetSection(sectionName ?? typeof(T).Name));

    public static T GetOptions<T>(this IConfiguration configuration, string? sectionName = null)
        => configuration
            .GetSection(sectionName ?? typeof(T).Name)
            .Get<T>() ?? throw new OptionsNotFoundException(sectionName ?? typeof(T).Name);
}