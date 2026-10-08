using Microsoft.Extensions.DependencyInjection;

namespace OsmPlayground.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOsmData(this IServiceCollection services)
    {
        return services;
    }
}
