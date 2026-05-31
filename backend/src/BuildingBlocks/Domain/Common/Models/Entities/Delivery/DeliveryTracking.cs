using TMS.Domain.Common.Models.Enums.Delivery;
using TMS.Domain.Common.Models.Entities;

namespace TMS.Domain.Common.Models.Entities.Delivery;

public sealed class DeliveryTracking : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid? DelivererId { get; private set; }
    public string? RouteGraphId { get; private set; }
    public string? RouteJson { get; private set; }
    public DeliveryTrackingStatus Status { get; private set; } = DeliveryTrackingStatus.AwaitingDeliverer;

    public string DeliveryCity { get; private set; } = string.Empty;
    public string DeliveryStreet { get; private set; } = string.Empty;
    public string DeliveryBuilding { get; private set; } = string.Empty;
    public double? DeliveryLatitude { get; private set; }
    public double? DeliveryLongitude { get; private set; }

    private DeliveryTracking()
    {
    }

    public static DeliveryTracking CreateAwaiting(
        Guid orderId,
        string city,
        string street,
        string building,
        double? latitude,
        double? longitude) =>
        new()
        {
            OrderId = orderId,
            DeliveryCity = city,
            DeliveryStreet = street,
            DeliveryBuilding = building,
            DeliveryLatitude = latitude,
            DeliveryLongitude = longitude,
            Status = DeliveryTrackingStatus.AwaitingDeliverer,
            CreatedAtUtc = DateTime.UtcNow
        };

    public void AssignDelivererAndRoute(Guid delivererId, string routeId, string routeJson)
    {
        DelivererId = delivererId;
        RouteGraphId = routeId;
        RouteJson = routeJson;
        Status = DeliveryTrackingStatus.RoutePlanned;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void UpdateStatus(DeliveryTrackingStatus status)
    {
        Status = status;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
