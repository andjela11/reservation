using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class PersistenceServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseMySQL(configuration.GetConnectionString("Default"),
                x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().ToString()));
        });

        return services;
    }
}
