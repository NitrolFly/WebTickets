using WebTickets.Domain;

namespace WebTickets.Domain.Modules;

public record RoleId 
{
    public Guid Value { get; }

    private RoleId(Guid value)
    {
        Value = value;
    }

    public static RoleId NewRoleId() => new(Guid.NewGuid());
    public static RoleId Empty() => new(Guid.Empty);
    public static RoleId Create(Guid id) => new(id);
}