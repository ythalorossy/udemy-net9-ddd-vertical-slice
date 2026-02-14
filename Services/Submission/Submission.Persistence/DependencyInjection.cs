using Articles.Abstractions.Enums;
using Blocks.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Domain.Entities;
using Submission.Persistence.Repositories;

namespace Submission.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext with connection string from configuration
        services.AddDbContext<SubmissionDbContext>(
            (provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("Database");

            }
        );

        services.AddScoped(typeof(Repository<>));
        services.AddScoped<ArticleRepository>();

        services.AddScoped<CachedRepository<SubmissionDbContext, AssetTypeDefinition, AssetType>>();

        return services;
    }
}
