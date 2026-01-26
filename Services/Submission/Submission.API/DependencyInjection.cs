using FileStorage.MongoGridFS;

namespace Submission.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddMemoryCache()                   // Basic Caching
            .AddEndpointsApiExplorer()          // MinimalAPI DOcs (Swagger)
            .AddSwaggerGen();                   // Swagger setup

        services
            .AddMongoFileStorage(configuration);

        return services;
    }
}
