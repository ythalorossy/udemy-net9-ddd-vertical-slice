using Blocks.MediatR.Behaviors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Submission.Application.Features.CreateArticle;
using System.Reflection;

namespace Submission.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddValidatorsFromAssemblyContaining<CreateArticleCommandValidator>() // Register FluentValidation validators as transient
            .AddMediatR(cfg =>
            {
                // Register all services in this assembly (Mediatr, ...)
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                // The order of the behaviors matters
                cfg
                    .AddOpenBehavior(typeof(ValidationBehavior<,>))     // Validate Request
                    .AddOpenBehavior(typeof(SetUserIdBehavior<,>));     // Set User ID to the Command


            });

        return services;
    }
}