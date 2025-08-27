
using System.Security.Cryptography;
using System.Text;
using comerciales.Application.Abstractions;

namespace comerciales.Infrastructure.Security;

public class Sha256PasswordHasher : IPasswordHasher
{
    public bool Verify(string plainPassword, byte[] dbHash)
    {
        using var sha = SHA256.Create();
        var computed = sha.ComputeHash(Encoding.UTF8.GetBytes(plainPassword));
        return dbHash.SequenceEqual(computed);
    }

}
