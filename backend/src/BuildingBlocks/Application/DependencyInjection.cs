using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace TMS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediator(static (MediatorOptions options) =>
        {
            options.Assemblies = [typeof(ApplicationAssembly)];
            options.ServiceLifetime = ServiceLifetime.Scoped;
        });

        return services;
    }
}
