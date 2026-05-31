using TMS.Domain.Common;
using TMS.Domain.Enums;

namespace TMS.Domain.Entities;

public sealed class DeliveryTracking : BaseEntity
{
    public Guid OrderId { get; private set; }

    public Guid? DelivererId { get; private set; }

    public DeliveryTrackingStatus Status { get; private set; } = DeliveryTrackingStatus.AwaitingDeliverer;

    private DeliveryTracking()
    {
    }

    public static DeliveryTracking CreateAwaiting(Guid orderId) =>
        new()
        {
            OrderId = orderId,
            Status = DeliveryTrackingStatus.AwaitingDeliverer,
            CreatedAtUtc = DateTime.UtcNow
        };

    public void AssignDeliverer(Guid delivererId)
    {
        DelivererId = delivererId;
        Status = DeliveryTrackingStatus.RoutePlanned;
        MarkUpdated();
    }

    public void UpdateStatus(DeliveryTrackingStatus status)
    {
        Status = status;
        MarkUpdated();
    }
}
