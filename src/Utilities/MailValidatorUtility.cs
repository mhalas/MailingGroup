using System.Net.Mail;

namespace Utilities
{
    /// <summary>
    /// Useful Link
    /// https://www.rfc-editor.org/errata/eid1690
    /// </summary>
    /// <returns></returns>
    public class MailValidatorUtility
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
