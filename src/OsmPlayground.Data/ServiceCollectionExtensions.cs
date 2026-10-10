using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsmPlayground.Data.Context;

namespace OsmPlayground.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOsmData(this IServiceCollection services)
    {
        services.AddDbContext<OsmDbContext>((serviceProvider, options) =>
        {
            var connectionString = serviceProvider
                .GetRequiredService<IConfiguration>()
                .GetConnectionString(OsmDbContext.ConnectionStringName)
                ?? throw new InvalidOperationException(
                    $"Connection string '{OsmDbContext.ConnectionStringName}' is not configured.");

            options.UseNpgsql(connectionString, npgsql => npgsql.UseNetTopologySuite());
        });

        return services;
    }
}
