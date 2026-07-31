using FLPTech.Blog.Domain.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FLPTech.Blog.Infraestructure.Extensions;

public static class InfraestructureConfig
{
    public static IServiceCollection AddInfraestructureConfigs(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
