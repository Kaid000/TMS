using TMS.Domain.Common;

namespace TMS.Domain.Entities;

public class DelivererOrderResponse : BaseEntity
{
    public Guid OrderId { get; private set; }

    public Guid DelivererId { get; private set; }

    public string? Comment { get; private set; }

    private DelivererOrderResponse()
    {
    }

    public static DelivererOrderResponse Create(Guid orderId, Guid delivererId, string? comment) =>
        new()
        {
            OrderId = orderId,
            DelivererId = delivererId,
            Comment = string.IsNullOrWhiteSpace(comment) ? null : comment.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        };
}
