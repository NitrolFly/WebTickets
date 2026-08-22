using CSharpFunctionalExtensions;
using WebTickets.Application.Abstractions.Security;
using WebTickets.Domain;
using WebTickets.Domain.Module.User;
using WebTickets.Domain.Modules;

namespace WebTickets.Application.Services;

public class UserRegistrationService
{
    private readonly IPasswordHasher _hasher;

    public UserRegistrationService(IPasswordHasher hasher)
    {
        _hasher = hasher;
    }

    public async Task<Result<User>> Register(string userName, string email, string rawPassword, RoleId roleId)
    {
        if (string.IsNullOrWhiteSpace(rawPassword) || rawPassword.Length < Constants.MIN_PASSWORD_LENGTH)
            return Result.Failure<User>($"Password must be at least {Constants.MIN_PASSWORD_LENGTH} characters long");

        var passwordHash = await _hasher.HashAsync(rawPassword);
        var user = User.Create(userName, email, passwordHash, roleId);

        return Result.Success(user);
    }

    public async Task<bool> CheckPassword(User user, string rawPassword)
    {
        return await _hasher.VerifyAsync(rawPassword, user.PasswordHash);
    }
}