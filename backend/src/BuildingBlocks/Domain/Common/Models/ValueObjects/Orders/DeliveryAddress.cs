namespace TMS.Domain.Common.Models.ValueObjects.Orders;

public sealed class DeliveryAddress
{
    public string City { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string Building { get; private set; } = string.Empty;
    public string? Apartment { get; private set; }
    public string PostalCode { get; private set; } = string.Empty;
    public string? Comment { get; private set; }
    public double? Latitude { get; private set; }
    public double? Longitude { get; private set; }

    private DeliveryAddress()
    {
    }

    public DeliveryAddress(
        string city,
        string street,
        string building,
        string postalCode,
        string? apartment = null,
        string? comment = null,
        double? latitude = null,
        double? longitude = null)
    {
        City = city;
        Street = street;
        Building = building;
        PostalCode = postalCode;
        Apartment = apartment;
        Comment = comment;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string ToDisplayString() =>
        string.Join(", ",
            new[] { City, Street, Building, Apartment, PostalCode }
                .Where(part => !string.IsNullOrWhiteSpace(part)));
}
