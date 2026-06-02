using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TMS.Infrastructure.Persistence.DbContext;

/// <summary>
/// Design-time factory for EF Core CLI (migrations).
/// </summary>
public sealed class TmsDbContextFactory : IDesignTimeDbContextFactory<TmsDbContext>
{
    private const string DefaultConnectionString =
        "Host=localhost;Port=5432;Database=tms;Username=postgres;Password=postgres";

    public TmsDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("TMS_CONNECTION_STRING")
            ?? DefaultConnectionString;

        var optionsBuilder = new DbContextOptionsBuilder<TmsDbContext>();
        optionsBuilder.UseNpgsql(connectionString, static builder =>
            builder.MigrationsAssembly(typeof(TmsDbContext).Assembly.FullName));

        return new TmsDbContext(optionsBuilder.Options);
    }
}
