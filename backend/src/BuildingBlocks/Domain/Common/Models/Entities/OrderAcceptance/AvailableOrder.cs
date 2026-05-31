namespace TMS.Domain.Common.Models.Entities.OrderAcceptance;

public sealed class AvailableOrder : BaseEntity
{
    public Guid OrderId { get; private set; }

    public Guid CustomerId { get; private set; }

    public decimal TotalAmount { get; private set; }

    public decimal Fees { get; private set; }

    public string OrderStatus { get; private set; } = string.Empty;

    public string PaymentMethod { get; private set; } = string.Empty;

    public string City { get; private set; } = string.Empty;

    public string Street { get; private set; } = string.Empty;

    public string Building { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;

    public string? Apartment { get; private set; }

    public string? AddressComment { get; private set; }

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public int ItemCount { get; private set; }

    public bool IsOpenForResponses { get; private set; } = true;

    public DateTime PlacedAtUtc { get; private set; }

    private AvailableOrder()
    {
    }

    public static AvailableOrder FromPlacedEvent(
        Guid orderId,
        Guid customerId,
        decimal totalAmount,
        decimal fees,
        string orderStatus,
        string paymentMethod,
        string city,
        string street,
        string building,
        string postalCode,
        string? apartment,
        string? addressComment,
        double? latitude,
        double? longitude,
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
            City = city,
            Street = street,
            Building = building,
            PostalCode = postalCode,
            Apartment = apartment,
            AddressComment = addressComment,
            Latitude = latitude,
            Longitude = longitude,
            ItemCount = itemCount,
            PlacedAtUtc = placedAtUtc,
            CreatedAtUtc = DateTime.UtcNow,
            IsOpenForResponses = true
        };

    public void CloseForResponses()
    {
        IsOpenForResponses = false;
        Touch();
    }
}
