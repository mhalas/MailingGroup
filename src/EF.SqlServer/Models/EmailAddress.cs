using System;
using System.Collections.Generic;

#nullable disable

namespace EF.SqlServer.Models
{
    public partial class EmailAddress
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int MailingGroupId { get; set; }

        public virtual MailingGroup MailingGroup { get; set; }
    }
}
