using TMS.Domain.Common.Models.Enums.Identity;

namespace TMS.Domain.Common.Models.Entities.Identity;

public sealed class Customer : User
{
    private Customer()
    {
        Kind = UserKind.Customer;
    }

    internal Customer(string email, string passwordHash, string phone, string displayName)
        : base(UserKind.Customer, email, passwordHash, phone, displayName)
    {
    }
}
