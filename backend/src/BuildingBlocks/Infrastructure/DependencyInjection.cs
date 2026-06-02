using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TMS.Infrastructure.Persistence.DbContext;

namespace TMS.Infrastructure;

public static class DependencyInjection
{
    public const string DefaultConnectionStringName = "DefaultConnection";

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnectionStringName)
            ?? throw new InvalidOperationException(
                $"Connection string '{DefaultConnectionStringName}' is not configured.");

        services.AddDbContext<TmsDbContext>(options =>
            options.UseNpgsql(connectionString, static npgsql =>
                npgsql.MigrationsAssembly(typeof(TmsDbContext).Assembly.FullName)));

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddDbContext<TmsDbContext>(options =>
            options.UseNpgsql(connectionString, static npgsql =>
                npgsql.MigrationsAssembly(typeof(TmsDbContext).Assembly.FullName)));

        return services;
    }
}
