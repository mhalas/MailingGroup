using Contracts.Utility;
using System.Net.Mail;

namespace Utilities
{
    /// <summary>
    /// I could use regex for validate email addresses, but 100% currect regex is unreadable.
    /// http://www.ex-parrot.com/pdw/Mail-RFC822-Address.html
    /// 
    /// Also:
    /// https://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx/
    /// https://en.wikipedia.org/wiki/Email_address#Local-part
    /// </summary>
    public class EmailAddressValidatorUtility: IEmailAddressValidatorUtility
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
