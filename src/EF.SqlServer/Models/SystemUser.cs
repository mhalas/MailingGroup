using System;
using System.Collections.Generic;

#nullable disable

namespace EF.SqlServer.Models
{
    public partial class SystemUser
    {
        public SystemUser()
        {
            MailingGroups = new HashSet<MailingGroup>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public virtual ICollection<MailingGroup> MailingGroups { get; set; }
    }
}
