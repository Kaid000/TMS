using TMS.Domain.Common;

namespace TMS.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string Make { get; private set; } = string.Empty;

    public string Model { get; private set; } = string.Empty;

    public string LicensePlate { get; private set; } = string.Empty;

    public int? YearOfManufacture { get; private set; }

    public string? Color { get; private set; }

    private Vehicle()
    {
    }

    public Vehicle(
        string make,
        string model,
        string licensePlate,
        string? color = null,
        int? yearOfManufacture = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(make);
        ArgumentException.ThrowIfNullOrWhiteSpace(model);
        ArgumentException.ThrowIfNullOrWhiteSpace(licensePlate);

        Make = make;
        Model = model;
        LicensePlate = licensePlate;
        Color = color;
        YearOfManufacture = yearOfManufacture;
    }

    public string DisplayName => $"{Make} {Model} ({LicensePlate})";
}
