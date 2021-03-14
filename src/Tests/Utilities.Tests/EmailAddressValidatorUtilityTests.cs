using NUnit.Framework;

namespace Utilities.Tests
{
    /// <summary>
    /// Test cases are copied from https://codefool.tumblr.com/post/15288874550/list-of-valid-and-invalid-email-addresses
    /// </summary>
    public class EmailAddressValidatorUtilityTests
    {
        [TestCase("email@example.com")]
        [TestCase("firstname.lastname@example.com")]
        [TestCase("email@subdomain.example.com")]
        [TestCase("firstname+lastname@example.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("“email”@example.com")]
        [TestCase("1234567890@example.com")]
        [TestCase("email@example-one.com")]
        [TestCase("_______@example.com")]
        [TestCase("email@example.name")]
        [TestCase("email@example.museum")]
        [TestCase("email@example.co.jp")]
        [TestCase("firstname-lastname@example.com")]
        public void Should_ReturnTrue_When_PassCorrectEmailAddress(string emailAddress)
        {
            var validator = new EmailAddressValidatorUtility();

            Assert.IsTrue(validator.ValidateMail(emailAddress));
        }

        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@example.com")]
        [TestCase("Joe Smith <email@example.com>")]
        [TestCase("email.example.com")]
        [TestCase("email@example@example.com")]
        [TestCase(".email@example.com")]
        [TestCase("email@example.com (Joe Smith)")]
        public void Should_ReturnFalse_When_PassIncorrectEmailAddress(string emailAddress)
        {
            var validator = new EmailAddressValidatorUtility();

            Assert.IsFalse(validator.ValidateMail(emailAddress));
        }
    }
}
