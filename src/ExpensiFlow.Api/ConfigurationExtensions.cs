namespace ExpensiFlow.Api;

public static class ConfigurationManagerExtensions
{
    public static string GetString(this ConfigurationManager configuration, string key) 
        => configuration[key] ?? throw new ArgumentException($"Value for {key} not found");
}