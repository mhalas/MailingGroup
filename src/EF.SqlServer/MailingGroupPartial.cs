using Contracts.Models;

namespace EF.SqlServer.Models
{
    public partial class MailingGroup : IMailingGroup
    {
        ISystemUser IMailingGroup.SystemUser { get => SystemUser; set => SystemUser = (SystemUser)value; }
    }
}
