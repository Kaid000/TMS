using TMS.Domain.Common.Models.Entities;

namespace TMS.Domain.Common.Models.Entities.Orders;

public sealed class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = null!;
    public string Name { get; private set; } = string.Empty;
    public string? Sku { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal LineTotal => UnitPrice * Quantity;

    private OrderItem()
    {
    }

    internal OrderItem(Guid orderId, string name, int quantity, decimal unitPrice, string? sku = null)
    {
        OrderId = orderId;
        Name = name;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Sku = sku;
    }
}
