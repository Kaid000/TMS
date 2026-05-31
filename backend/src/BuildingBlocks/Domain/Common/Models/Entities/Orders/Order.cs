using TMS.Domain.Common.Models.Entities.Identity;
using TMS.Domain.Common.Models.Enums.Orders;
using TMS.Domain.Common.Models.ValueObjects.Orders;

namespace TMS.Domain.Common.Models.Entities.Orders;

public sealed class Order : BaseEntity
{
    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; } = null!;

    public Guid? DelivererId { get; private set; }

    public Deliverer? Deliverer { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Draft;

    public PaymentMethod PaymentMethod { get; private set; }

    public DeliveryAddress DeliveryAddress { get; private set; } = null!;

    public decimal ItemsTotal { get; private set; }

    public decimal Fees { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string? AssembledOrderPhotoId { get; private set; }

    public string? RouteGraphId { get; private set; }

    private readonly List<OrderItem> _items = [];

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order()
    {
    }

    public static Order Create(
        Customer customer,
        PaymentMethod paymentMethod,
        DeliveryAddress deliveryAddress,
        IEnumerable<(string Name, int Quantity, decimal UnitPrice, string? Sku)> items,
        decimal fees)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(deliveryAddress);

        var order = new Order
        {
            CustomerId = customer.Id,
            Customer = customer,
            PaymentMethod = paymentMethod,
            DeliveryAddress = deliveryAddress,
            Fees = fees,
            Status = OrderStatus.Placed
        };

        foreach (var item in items)
        {
            order._items.Add(new OrderItem(order.Id, item.Name, item.Quantity, item.UnitPrice, item.Sku));
        }

        order.RecalculateTotals();
        return order;
    }

    public void AssignDelivererId(Guid delivererId)
    {
        DelivererId = delivererId;
        Touch();
    }

    public void MarkAssembled(string photoId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(photoId);

        AssembledOrderPhotoId = photoId;
        Status = OrderStatus.Assembled;
        Touch();
    }

    public void AttachRoute(string routeGraphId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(routeGraphId);

        RouteGraphId = routeGraphId;
        Touch();
    }

    public void RecalculateTotals()
    {
        ItemsTotal = _items.Sum(i => i.LineTotal);
        TotalAmount = ItemsTotal + Fees;
        Touch();
    }
}
