namespace TMS.Domain.Common.Models.Enums.Delivery;

public enum DeliveryTrackingStatus
{
    AwaitingDeliverer = 1,
    RoutePlanned = 2,
    PickedUp = 3,
    InTransit = 4,
    Delivered = 5,
    Cancelled = 6
}
