using TMS.Domain.Common.Models.Enums.Identity;

namespace TMS.Domain.Common.Models.Entities.Identity;

public sealed class Deliverer : User
{
    public string VehicleInfo { get; private set; } = string.Empty;

    public bool IsAvailable { get; private set; } = true;

    private Deliverer()
    {
        Kind = UserKind.Deliverer;
    }

    private Deliverer(
        string email,
        string passwordHash,
        string phone,
        string displayName,
        string vehicleInfo)
        : base(UserKind.Deliverer, email, passwordHash, phone, displayName)
    {
        VehicleInfo = vehicleInfo;
    }

    public static Deliverer Register(
        string email,
        string passwordHash,
        string phone,
        string displayName,
        string vehicleInfo) =>
        new(email, passwordHash, phone, displayName, vehicleInfo);

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        Touch();
    }
}
