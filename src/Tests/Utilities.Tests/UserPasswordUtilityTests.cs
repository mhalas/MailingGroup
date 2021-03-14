using NUnit.Framework;
using System.Text;

namespace Utilities.Tests
{
    [TestFixture]
    public class UserPasswordUtilityTests
    {
        [TestCase("TestPassword", "vUT2c37jiiA=")]
        [TestCase("TestPassword1", "jHCaNisYhos=")]
        [TestCase("BestPasswordGeneratorEver", "qg4PMFZIa+g=")]
        public void Should_ReturnFalse_when_CheckingPasswordCorrectnessOnDifferentPassword(string password, string hashedDifferentPassword)
        {
            var userPasswordUtility = new UserPasswordUtility(iterations: 1, keySize: 8);

            var salt = Encoding.ASCII.GetBytes("UserPasswordTest");

            var isPasswordNotTheSameAsOriginPassword = userPasswordUtility.IsPasswordCorrect(password, salt, hashedDifferentPassword);

            Assert.IsFalse(isPasswordNotTheSameAsOriginPassword);
        }

        [TestCase("TestPassword", "jHCaNisYhos=")]
        [TestCase("TestPassword1", "qg4PMFZIa+g=")]
        [TestCase("BestPasswordGeneratorEver", "vUT2c37jiiA=")]
        public void Should_ReturnTrue_when_CheckingPasswordCorrectness(string password, string hashedPassword)
        {
            var userPasswordUtility = new UserPasswordUtility(iterations: 1, keySize: 8);

            var salt = Encoding.ASCII.GetBytes("UserPasswordTest");

            var isPasswordTheSameAsOriginPassword = userPasswordUtility.IsPasswordCorrect(password, salt, hashedPassword);

            Assert.IsTrue(isPasswordTheSameAsOriginPassword);
        }

        [TestCase("TestPassword", "jHCaNisYhos=")]
        [TestCase("TestPassword1", "qg4PMFZIa+g=")]
        [TestCase("BestPasswordGeneratorEver", "vUT2c37jiiA=")]
        public void Should_ReturnHashedPassword_when_PassUnhashedPassword(string password, string correctHashedPassword)
        {
            var userPasswordUtility = new UserPasswordUtility(iterations: 1, keySize: 8);

            var salt = Encoding.ASCII.GetBytes("UserPasswordTest");
            var hashedPassword = userPasswordUtility.HashPassword(password, salt);

            Assert.AreEqual(correctHashedPassword, hashedPassword);
        }
    }
}
