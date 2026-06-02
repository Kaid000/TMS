using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TMS.Infrastructure.Persistence.DbContext;

/// <summary>
/// Design-time factory for EF Core CLI (migrations).
/// </summary>
public sealed class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
{
    public TmsDbContext CreateDbContext(string[] args)
    {
        // Environment override for local/dev/test and Docker Compose compatibility
        var connectionString = Environment.GetEnvironmentVariable("TMS_CONNECTION_STRING");

        // Read from appsettings*.json in the working directory (typically the "startup" project
        // when `dotnet ef` is executed).
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();

            connectionString = configuration.GetConnectionString(
                TMS.Infrastructure.DependencyInjection.DefaultConnectionStringName);
        }

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException(
                $"Connection string '{TMS.Infrastructure.DependencyInjection.DefaultConnectionStringName}' is not configured. " +
                $"Set it in appsettings.json (ConnectionStrings section) or via TMS_CONNECTION_STRING env var.");

        var optionsBuilder = new DbContextOptionsBuilder<TmsDbContext>();
        optionsBuilder.UseNpgsql(connectionString, static builder =>
            builder.MigrationsAssembly(typeof(TmsDbContext).Assembly.FullName));

        return new TmsDbContext(optionsBuilder.Options);
    }
}
