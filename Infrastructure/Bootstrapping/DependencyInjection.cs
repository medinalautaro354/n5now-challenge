using Application.ConfigurationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5NowApi.Infrastructure.Boopstrap;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Infrastructure.Behaviours;

namespace Infrastructure.Bootstrapping;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.SqlServerConfiguration(configuration);

        var kafkaConfiguration = new KafkaConfiguration(
            configuration["AMQStreams:BootstrapServer"],
            configuration["Topics:Permissions"]
        );
        services.AddSingleton(kafkaConfiguration);
        
        services.AddKafka(configuration)
            .CreateTopic(3, (short)1, configuration["Topics:Permissions"]);

        services.RepositoriesInjector();

        services.ServicesInjector();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

        return services;
    }
}

