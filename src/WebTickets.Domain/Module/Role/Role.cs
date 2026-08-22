using WebTickets.Domain.Modules;
using WebTickets.Domain.Shared;

namespace WebTickets.Domain.Roles;

public class Role : Entity<RoleId>
{
    private Role() : base(default!) { }
    public string RoleName { get; private set; } = default!;

    private readonly List<Permission> _permissons = new();
    public IReadOnlyCollection<Permission> Permissions => _permissons.AsReadOnly();
    public enum Permission
    {
        ViewTickets,
        AssignTickets,
        CloseTickets,
        ManageUsers,
        ManageRoles
    }
    private Role(RoleId id, string roleName) : base(id)
    {
        RoleName = roleName;
    }

    public static Role Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new AggregateException("Name is not be empty");

        return new Role(RoleId.NewRoleId(), name);
    }

    public void AddPermission(Permission permission)
    {
        if (!_permissons.Contains(permission))
            _permissons.Add(permission);
    }

    public void RemovePermission(Permission permission) => _permissons.Remove(permission);

    public bool HasPermission(Permission permission) => _permissons.Contains(permission);
   
}