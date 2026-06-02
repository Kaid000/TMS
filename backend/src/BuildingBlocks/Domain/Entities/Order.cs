using TMS.Domain.Common;
using TMS.Domain.Enums;
using TMS.Domain.ValueObjects;

namespace TMS.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; private set; }

    public Guid? DelivererId { get; private set; }

    public OrderStatus Status { get; private set; } = OrderStatus.Draft;

    public PaymentMethod PaymentMethod { get; private set; }


    public decimal ItemsTotal { get; private set; }

    public decimal Fees { get; private set; }

    public decimal TotalAmount { get; private set; }

    public string? AssembledOrderPhotoId { get; private set; }

    public string? RouteGraphId { get; private set; }

    public ICollection<OrderItem> OrderItems { get; private set; } = [];

    public Customer Customer { get; private set; } = null!;

    public Deliverer? Deliverer { get; private set; }
    
    public DeliveryAddress DeliveryAddress { get; private set; } = null!;

    private Order()
    {
    }

    public static Order Create(
        Customer customer,
        PaymentMethod paymentMethod,
        DeliveryAddress deliveryAddress,
        IEnumerable<(string Name, int Quantity, decimal UnitPrice, string? StockKeepingUnit)> items,
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
            order.OrderItems.Add(new OrderItem(
                order.Id,
                item.Name,
                item.Quantity,
                item.UnitPrice,
                item.StockKeepingUnit));
        }

        order.RecalculateTotals();
        return order;
    }

    public void AssignDelivererId(Guid delivererId)
    {
        DelivererId = delivererId;
        MarkUpdated();
    }

    public void MarkAssembled(string photoId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(photoId);

        AssembledOrderPhotoId = photoId;
        Status = OrderStatus.Assembled;
        MarkUpdated();
    }

    public void AttachRoute(string routeGraphId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(routeGraphId);

        RouteGraphId = routeGraphId;
        MarkUpdated();
    }

    public void RecalculateTotals()
    {
        ItemsTotal = OrderItems.Sum(i => i.LineTotal);
        TotalAmount = ItemsTotal + Fees;
        MarkUpdated();
    }
}
