﻿using Career.Consul.Options;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Career.Consul;

public static class ServiceCollectionExtensions
{
    public static void AddConsulServices(this IServiceCollection services, Action<ServiceDiscoveryOption> action)
    {
        var options = new ServiceDiscoveryOption();
        action.Invoke(options);
        if (options == null)
        {
            throw new ArgumentNullException(nameof(options));
        }

        var consulClient = CreateConsulClient(options);

        services.AddSingleton(options);
        services.AddSingleton<IHostedService, ConsulHostedService>();
        services.AddSingleton<IConsulClient, ConsulClient>(_ => consulClient);
    }

    private static ConsulClient CreateConsulClient(ServiceDiscoveryOption serviceConfig)
    {
        return new ConsulClient(config => { config.Address = new Uri(serviceConfig.ConsulUrl); });
    }
}