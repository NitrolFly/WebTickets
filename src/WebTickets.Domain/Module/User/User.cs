using System.Text.RegularExpressions;
using WebTickets.Domain.Modules;
using WebTickets.Domain.Shared;

namespace WebTickets.Domain.Module.User;

public class User : Entity<UserId>
{
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public RoleId RoleId { get; private set; } = default!;

    private User(UserId id, string userName, string email, string passwordHash, RoleId roleId) : base(id)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        RoleId = roleId;
    }

    private User() : base(UserId.Empty()) { }
    
    public static User Create(string userName, string passwordHash, string email, RoleId roleId)
    {
        var validUserName = ValidateUserName(userName);
        var validEmail = ValidateEmail(email);
        var validPasswordHash = ValidatePassword(passwordHash);
        
        
        return new User(UserId.NewUserId(), validUserName, validEmail, passwordHash, roleId);
    }

    public void SetPassword(string newPasswordHash)
    {
        PasswordHash = ValidatePassword(newPasswordHash);
    }
    public void ChangeRole(RoleId newRoleId) => RoleId = newRoleId;

    public void Rename(string newUserName)
    {
        UserName = ValidateUserName(newUserName);
    }

    private static string ValidatePassword(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password is not be empty", nameof(passwordHash));

        return passwordHash;
    }
    
    private static string ValidateUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new ArgumentException("UserName is not be empty", nameof(userName));
        }
        return userName;
    }
    private static string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            throw new ArgumentException("Invalid email address", nameof(email));

        return email.Trim().ToLowerInvariant();
    }
    private static bool IsValidEmail(string email) =>
        Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
}

