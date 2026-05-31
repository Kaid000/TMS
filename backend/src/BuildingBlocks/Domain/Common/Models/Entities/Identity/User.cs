using TMS.Domain.Common.Models.Enums.Identity;
using TMS.Domain.Common.Models.Entities;

namespace TMS.Domain.Common.Models.Entities.Identity;

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

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}
