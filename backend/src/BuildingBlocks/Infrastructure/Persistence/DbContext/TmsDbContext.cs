using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.DbContext;

public sealed class TmsDbContext(DbContextOptions<TmsDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Deliverer> Deliverers => Set<Deliverer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public DbSet<DeliveryTracking> DeliveryTrackings => Set<DeliveryTracking>();

    public DbSet<DelivererOrderResponse> DelivererOrderResponses => Set<DelivererOrderResponse>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH inheritance for User hierarchy — discriminator maps to the existing Kind column
        modelBuilder.Entity<User>()
            .HasDiscriminator(u => u.Kind)
            .HasValue<Customer>(TMS.Domain.Enums.UserKind.Customer)
            .HasValue<Deliverer>(TMS.Domain.Enums.UserKind.Deliverer);

        // DeliveryAddress as owned type (value object — no separate table)
        modelBuilder.Entity<Order>()
            .OwnsOne(o => o.DeliveryAddress);

        base.OnModelCreating(modelBuilder);
    }
}
