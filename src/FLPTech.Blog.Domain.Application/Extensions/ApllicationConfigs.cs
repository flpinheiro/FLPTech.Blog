using Cortex.Mediator.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FLPTech.Blog.Domain.Application.Extensions;

public static class ApllicationConfigs
{
    public static IServiceCollection AddApplicationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //add cortex mediator
        services.AddCortexMediator(
            new[] { typeof(AssemblyInfo) }, // Assemblies to scan for handlers
            options => options.AddDefaultBehaviors() // Logging
);
        return services;
    }
}
