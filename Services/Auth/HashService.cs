using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Services.Auth
{
    public class HashService : IHashService
    {

        public string GenerateSalt()
        {
            return GenerateRandomString(16);
        }

        public string GenerateToken()
        {
            return GenerateRandomString(48);
        }

        public string CalculateHash(string password, string salt)
        {
            byte[] bytes = Convert.FromBase64String(salt);
            string hashed = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: bytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 32 
                    )
                );
            return hashed;
        }

        public bool VerifyHash(string password, string salt, string hash)
        {
            if (
                string.IsNullOrEmpty(password) || 
                string.IsNullOrEmpty(salt) || 
                string.IsNullOrEmpty(hash)
                )
            {
                return false;
            }
            var actualHash = CalculateHash(password, salt);

            return CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(hash),
                Encoding.UTF8.GetBytes(actualHash)
                );
        }
        private static string GenerateRandomString(int size)
        {
            byte[] value = RandomNumberGenerator.GetBytes(size);
            return Convert.ToBase64String(value);

        }
    }
}
