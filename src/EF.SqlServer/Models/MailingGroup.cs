using System;
using System.Collections.Generic;

#nullable disable

namespace EF.SqlServer.Models
{
    public partial class MailingGroup
    {
        public MailingGroup()
        {
            Mail = new HashSet<Mail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SystemUserId { get; set; }

        public virtual SystemUser SystemUser { get; set; }
        public virtual ICollection<Mail> Mail { get; set; }
    }
}
