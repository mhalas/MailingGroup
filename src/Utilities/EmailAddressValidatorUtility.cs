using System.Net.Mail;

namespace Utilities
{
    /// <summary>
    /// I could use regex for validate email addresses, but 100% currect regex is unreadable.
    /// http://www.ex-parrot.com/pdw/Mail-RFC822-Address.html
    /// </summary>
    public class EmailAddressValidatorUtility
    {
        public bool ValidateMail(string emailAddress)
        {
            try
            {

                var email = new MailAddress(emailAddress);
                return email.Address == emailAddress;
            }
            catch
            {
                return false;
            }
        }
    }
}
