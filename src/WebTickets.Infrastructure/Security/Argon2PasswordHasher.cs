using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using WebTickets.Application.Abstractions.Security;

namespace WebTickets.Infrastructure.Security;

public sealed class Argon2PasswordHasher: IPasswordHasher
{
    private const int MemorySize = 65536;
    private const int Iterations = 4;
    private const int Parallelism = 1;
    private const int SaltSize = 32;
    private const int HashSize = 32;

    public async Task<string> HashAsync(string @string)
    {
        var saltBytes = GenerateSalt();

        var hashBytes = await GenerateHash(@string, saltBytes);

        var hash = new byte[HashSize + SaltSize];

        Array.Copy(hashBytes, 0, hash, 0, HashSize);
        Array.Copy(saltBytes, 0, hash, HashSize, SaltSize);

        return Convert.ToBase64String(hash);
    }

    public async Task<bool> VerifyAsync(string @string, string hashString)
    {
        var hashBytes = Convert.FromBase64String(hashString);

        var hash = new byte[HashSize];
        var salt = new byte[SaltSize];

        Array.Copy(hashBytes, 0, hash, 0, HashSize);
        Array.Copy(hashBytes, HashSize, salt, 0, SaltSize);

        var stringHash = await GenerateHash(@string, salt);

        return CryptographicOperations.FixedTimeEquals(stringHash, hash);
    }

    private async Task<byte[]> GenerateHash(string @string, byte[] salt)
    {
        var argon2id = new Argon2id(Encoding.UTF8.GetBytes(@string))
        {
            MemorySize = MemorySize,
            Iterations = Iterations,
            DegreeOfParallelism = Parallelism,
            Salt = salt
        };

        return await argon2id.GetBytesAsync(HashSize);
    }

    private byte[] GenerateSalt()
    {
        var bytes = new byte[SaltSize];

        using (var rnd = RandomNumberGenerator.Create())
        {
            rnd.GetBytes(bytes);
        }

        return bytes;
    }
}