using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthLogic
{
    internal static class PasswordHashing
    {
        public static async Task<bool> VerifyPassword(
            string password,
            string hash,
            string saltBase64)
        {
            byte[] salt = Convert.FromBase64String(saltBase64);
            string hashToVerify = await CalculateHash(password, salt);
            if (hashToVerify == null)
                throw new Exception("Could not generate password hash to verify");
            return hash == hashToVerify;
        }

        public static async Task<(string, string)> HashPassword(string password)
        {
            byte[] salt = await GenerateSalt128Bit();
            string hash = await CalculateHash(password, salt);
            string saltBase64 = Convert.ToBase64String(salt);
            if (saltBase64 == null || hash == null)
                throw new Exception("Could not generate password hash");
            return (hash, saltBase64);
        }

        private static async Task<byte[]> GenerateSalt128Bit()
        {
            byte[] salt = new byte[128 / 8];
            await Task.Run(() =>
            {
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
            });
            return salt;
        }

        private static async Task<string> CalculateHash(string password, byte[] salt)
        {
            string hash = null;
            await Task.Run(() =>
            {
                hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            });
            return hash;
        }
    }
}
