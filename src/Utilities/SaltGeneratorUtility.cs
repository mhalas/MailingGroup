using System.Security.Cryptography;

namespace Utilities
{
    public class SaltGeneratorUtility
    {
        private const int SALT_LENGTH = 16;

        public byte[] Generate()
        {
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_LENGTH];
            rNGCryptoServiceProvider.GetBytes(salt);
            return salt;
        }
    }
}
