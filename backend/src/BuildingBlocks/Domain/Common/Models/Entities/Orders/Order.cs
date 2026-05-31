using TMS.Domain.Common.Models.Enums.Orders;
using TMS.Domain.Common.Models.Entities;
using TMS.Domain.Common.Models.Entities.Identity;
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

    /// <summary>Сумма по позициям товаров.</summary>
    public decimal ItemsTotal { get; private set; }

    /// <summary>Сборы (доставка, сервис и т.д.).</summary>
    public decimal Fees { get; private set; }

    /// <summary>Итог к оплате: ItemsTotal + Fees.</summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>ObjectId фото собранного заказа в MongoDB GridFS.</summary>
    public string? AssembledOrderPhotoId { get; private set; }

    /// <summary>Идентификатор маршрута доставки (хранится в delivery_trackings).</summary>
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

    public void AssignDeliverer(Deliverer deliverer)
    {
        DelivererId = deliverer.Id;
        Deliverer = deliverer;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AssignDelivererId(Guid delivererId)
    {
        DelivererId = delivererId;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AttachAssembledPhoto(string mongoPhotoId)
    {
        AssembledOrderPhotoId = mongoPhotoId;
        Status = OrderStatus.Assembled;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void AttachRoute(string routeGraphId)
    {
        RouteGraphId = routeGraphId;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void RecalculateTotals()
    {
        ItemsTotal = _items.Sum(i => i.LineTotal);
        TotalAmount = ItemsTotal + Fees;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
