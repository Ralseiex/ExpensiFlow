using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensiFlow.Shared.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, MessageBusOptions options)
        => AddMessageBus(services, options, Assembly.GetCallingAssembly());

    public static IServiceCollection AddMessageBus(
        this IServiceCollection services,
        MessageBusOptions options,
        params Assembly[] assemblies)
        => services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.SetInMemorySagaRepositoryProvider();

            foreach (var assembly in assemblies)
            {
                x.AddConsumers(assembly);
                x.AddSagaStateMachines(assembly);
                x.AddSagas(assembly);
                x.AddActivities(assembly);
            }

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(options.Host, "/", h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });
            });
        });
}