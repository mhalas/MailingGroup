using Contracts.Utility;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace Utilities
{
    public class UserPasswordUtility: IUserPasswordUtility
    {
        private readonly int _iterations;
        private readonly int _keySize;

        public UserPasswordUtility(int iterations, int keySize)
        {
            _iterations = iterations;
            _keySize = keySize;
        }

        public string HashPassword(string passedPassword, byte[] userSalt)
        {
            var hashBytes = KeyDerivation.Pbkdf2(passedPassword, userSalt, KeyDerivationPrf.HMACSHA256, _iterations, _keySize);
            return Convert.ToBase64String(hashBytes);
        }

        public bool IsPasswordCorrect(string passwordToBeCheck, byte[] userSalt, string storedUserPassword)
        {
            var checkingPasswordHash = HashPassword(passwordToBeCheck, userSalt);
            return checkingPasswordHash == storedUserPassword;
        }
    }
}
