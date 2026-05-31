using TMS.Domain.Common.Models.Enums.Identity;

namespace TMS.Domain.Common.Models.Entities.Identity;

public sealed class Deliverer : User
{
    public Guid? VehicleId { get; private set; }

    public Vehicle? vehicle { get; private set; }

    private Deliverer()
    {
        Kind = UserKind.Deliverer;
    }

    internal Deliverer(
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
