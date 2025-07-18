﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServiceDefaults.Messaging;

public static class MassTransitExtension
{
    public static IServiceCollection AddMassTransitWithAssemblies(this IServiceCollection service,
        params Assembly[] assemblies)
    {
        service.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();
            config.SetInMemorySagaRepositoryProvider();

            config.AddConsumers(assemblies);
            config.AddSagaStateMachines(assemblies);
            config.AddSagas(assemblies);
            config.AddActivities(assemblies);

            config.UsingRabbitMq((context, configurator) =>
            {
                var configuration = context.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("rabbitmq");

                configurator.Host(connectionString);
                configurator.ConfigureEndpoints(context);
            });
        });

        return service;
    }
}
