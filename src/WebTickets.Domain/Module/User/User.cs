using System.Text.RegularExpressions;
using WebTickets.Domain.Modules;
using WebTickets.Domain.Shared;

namespace WebTickets.Domain.Users;

public class User : Entity<UserId>
{
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public RoleId RoleId { get; private set; } = default!;

    private User(UserId id, string userName, string email, RoleId roleId) : base(id)
    {
        UserName = userName;
        Email = email;
        RoleId = roleId;
    }

    private User() : base(UserId.Empty()) { }
    
    
    public static User Create(string userName, string email, RoleId roleId)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName is not be empty", nameof(userName));

        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            throw new ArgumentException("Invalid email address", nameof(email));

        return new User(UserId.NewUserId(), userName, email.Trim().ToLowerInvariant(), roleId);
    }

    public void ChangeRole(RoleId newRoleId) => RoleId = newRoleId;

    public void Rename(string newUserName)
    {
        if (string.IsNullOrWhiteSpace(newUserName))
            throw new ArgumentException("UserName is not be empty", nameof(newUserName));

        UserName = newUserName;
    }
    private static bool IsValidEmail(string email) =>
        Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}