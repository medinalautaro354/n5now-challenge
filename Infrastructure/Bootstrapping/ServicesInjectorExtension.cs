using Application.Common.Services;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bootstrapping
{
    public static class ServicesInjectorExtension
    {
        public static IServiceCollection ServicesInjector(this IServiceCollection services)
        {
            services.AddTransient<IPublisherService, PublisherService>();

            return services;
        }

    }
}
