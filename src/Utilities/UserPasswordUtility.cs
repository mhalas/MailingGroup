using System;
using System.Security.Cryptography;

namespace Utilities
{
    public class UserPasswordUtility
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
            using (var algorithm = new Rfc2898DeriveBytes(passedPassword, userSalt, _iterations, HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(_keySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{key}{salt}";
            }
        }

        public bool IsPasswordCorrect(string passwordToBeCheck, byte[] userSalt, string storedUserPassword)
        {
            var checkingPasswordHash = HashPassword(passwordToBeCheck, userSalt);

            return checkingPasswordHash == storedUserPassword;
        }
    }
}
