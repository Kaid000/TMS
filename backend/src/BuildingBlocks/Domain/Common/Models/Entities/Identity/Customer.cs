using TMS.Domain.Common.Models.Enums.Identity;
using TMS.Domain.Common.Models.Entities.Orders;

namespace TMS.Domain.Common.Models.Entities.Identity;

/// <summary>
/// Заказчик. Регистрация только с данными для оформления заказов.
/// </summary>
public sealed class Customer : User
{
    private readonly List<Order> _orders = [];

    public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

    private Customer()
    {
        Kind = UserKind.Customer;
    }

    private Customer(string email, string passwordHash, string phone, string displayName)
        : base(UserKind.Customer, email, passwordHash, phone, displayName)
    {
    }

    public static Customer Register(string email, string passwordHash, string phone, string displayName) =>
        new(email, passwordHash, phone, displayName);
}
