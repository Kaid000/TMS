using TMS.Domain.Common;
using TMS.Domain.Enums;

namespace TMS.Domain.Entities;

public abstract class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Phone { get; private set; } = string.Empty;

    public string DisplayName { get; private set; } = string.Empty;

    public UserKind Kind { get; protected set; }

    public bool IsActive { get; private set; } = true;

    protected User()
    {
    }

    protected User(UserKind kind, string email, string passwordHash, string phone, string displayName)
    {
        Kind = kind;
        Email = email;
        PasswordHash = passwordHash;
        Phone = phone;
        DisplayName = displayName;
    }
}
