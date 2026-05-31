using TMS.Domain.Enums;

namespace TMS.Domain.Entities;

public sealed class Customer : User
{
    private Customer()
    {
        Kind = UserKind.Customer;
    }

    public Customer(string email, string passwordHash, string phone, string displayName)
        : base(UserKind.Customer, email, passwordHash, phone, displayName)
    {
    }
}
