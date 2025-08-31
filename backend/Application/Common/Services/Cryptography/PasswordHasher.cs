using System.Security.Cryptography;
using System.Text;

namespace Expense.Tracker.Application.Common.Services.Cryptography
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
    #region
    public class PasswordHasher : IPasswordHasher
    {
        public const int SaltSize = 32; // 128 bit
        public const int KeySize = 32; // 256 bit
        public const int Iterations = 350000; // Number of iterations for PBKDF2
        public static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),salt, Iterations, HashAlgorithm, KeySize);

            return $"{Convert.ToHexString(hash)}.{Convert.ToHexString(salt)}";
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split('.', 2);
            if (parts.Length != 2)
            {
                throw new FormatException("Unexpected hash format. Should be formatted as '{hash}.{salt}'");
            }
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] derivedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, derivedHash);
        }
    }
    #endregion
}
