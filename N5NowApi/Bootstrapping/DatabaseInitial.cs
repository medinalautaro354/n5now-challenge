using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace N5NowApi.Bootstrapping;

public static class DatabaseInitial
{
    public static void ExecuteMigrate(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            Migrate(serviceScope.ServiceProvider.GetService<ApplicationDbContext>());
        }
    }

    private static void Migrate(ApplicationDbContext context)
    {
        System.Console.WriteLine("Applying initial migration...");
        context.Database.Migrate();
        System.Console.WriteLine("Initial migration (database) done!");
    }
}