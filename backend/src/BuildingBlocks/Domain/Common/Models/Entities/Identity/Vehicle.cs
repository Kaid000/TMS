namespace TMS.Domain.Common.Models.Entities.Identity;

public sealed class Vehicle : BaseEntity
{
    public string Description { get; private set; } = string.Empty;

    private Vehicle()
    {
    }

    internal Vehicle(string description)
    {
        Description = description;
    }
}
