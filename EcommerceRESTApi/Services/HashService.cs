using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using EcommerceRESTApi.DTOs.Secutiry;
using System.Security.Cryptography;

namespace EcommerceRESTApi.Services
{
    public class HashService
    {
        public HashResult Hash(string planeText)
        {
            var sal = new byte[16];

            using (var random = RandomNumberGenerator.Create())
            {

                random.GetBytes(sal);
            }

            return Hash(planeText, sal);
        }


        public HashResult Hash(string planeText, byte[] sal)
        {
            var keyDerivation = KeyDerivation.Pbkdf2(password: planeText,
                salt: sal,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 1000,
                numBytesRequested: 32);

            var hash = Convert.ToBase64String(keyDerivation);

            return new HashResult()
            {
                Hash = hash,
                Sal = sal,
            };
        }
    }
}
