using TMS.Domain.Common;

namespace TMS.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? StockKeepingUnit { get; private set; }

    public int Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal LineTotal => UnitPrice * Quantity;

    public Order Order { get; private set; } = null!;

    private OrderItem()
    {
    }

    public OrderItem(
        Guid orderId,
        string name,
        int quantity,
        decimal unitPrice,
        string? stockKeepingUnit = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        if (unitPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(unitPrice));

        OrderId = orderId;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        StockKeepingUnit = stockKeepingUnit;
    }
}
