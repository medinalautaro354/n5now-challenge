using N5NowApi.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Persistance;

namespace N5NowApi.Infrastructure.Boopstrap;
public static class RepositoriesInjectorExtesion
{
    public static IServiceCollection RepositoriesInjector(this IServiceCollection services)
    {
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}

