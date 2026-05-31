using TMS.Domain.Common.Models.Enums.Orders;

namespace TMS.Domain.Common.Models.Entities.OrderAcceptance;

public sealed class AvailableOrder : BaseEntity
{
    public Guid OrderId { get; private set; }

    public Guid CustomerId { get; private set; }

    public decimal TotalAmount { get; private set; }

    public decimal Fees { get; private set; }

    public OrderStatus OrderStatus { get; private set; }

    public PaymentMethod PaymentMethod { get; private set; }

    public int ItemCount { get; private set; }

    public DateTime PlacedAtUtc { get; private set; }

    private AvailableOrder()
    {
    }

    public static AvailableOrder FromPlacedEvent(
        Guid orderId,
        Guid customerId,
        decimal totalAmount,
        decimal fees,
        OrderStatus orderStatus,
        PaymentMethod paymentMethod,
        int itemCount,
        DateTime placedAtUtc) =>
        new()
        {
            OrderId = orderId,
            CustomerId = customerId,
            TotalAmount = totalAmount,
            Fees = fees,
            OrderStatus = orderStatus,
            PaymentMethod = paymentMethod,
            ItemCount = itemCount,
            PlacedAtUtc = placedAtUtc,
            CreatedAtUtc = DateTime.UtcNow
        };
}
