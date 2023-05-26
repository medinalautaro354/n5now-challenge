using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bootstrapping;

public static class SqlServerConfigurationExtension
{
    public static IServiceCollection SqlServerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                
            });

        services.AddScoped<ApplicationDbContext>();
        
        return services;
    }
}