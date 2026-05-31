using TMS.Domain.Enums;

namespace TMS.Domain.Entities;

public sealed class Deliverer : User
{
    public Guid? VehicleId { get; private set; }

    public Vehicle? vehicle { get; private set; }

    private Deliverer()
    {
        Kind = UserKind.Deliverer;
    }

    public Deliverer(
        string email,
        string passwordHash,
        string phone,
        string displayName,
        Guid? vehicleId)
        : base(UserKind.Deliverer, email, passwordHash, phone, displayName)
    {
        VehicleId = vehicleId;
    }
}
