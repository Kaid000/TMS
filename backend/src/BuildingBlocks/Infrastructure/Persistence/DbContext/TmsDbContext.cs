using Microsoft.EntityFrameworkCore;
using TMS.Domain.Entities;

namespace TMS.Infrastructure.Persistence.DbContext;

public sealed class TmsDbContext(DbContextOptions<TmsDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Deliverer> Deliverers => Set<Deliverer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public DbSet<DeliveryTracking> DeliveryTrackings => Set<DeliveryTracking>();

    public DbSet<DelivererOrderResponse> DelivererOrderResponses => Set<DelivererOrderResponse>();
}
