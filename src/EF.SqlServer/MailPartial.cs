using Contracts.Models;

namespace EF.SqlServer.Models
{
    public partial class Mail : IMail
    {
        IMailingGroup IMail.MailingGroup { get => MailingGroup; set => MailingGroup = (MailingGroup)value; }
    }
}
