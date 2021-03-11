using Contracts.Dto;
using System.Collections.Generic;
using System.Net;

namespace Contracts.Responses
{
    public class RetrieveMailingGroupsResponse : BasicResponseInfo
    {
        public RetrieveMailingGroupsResponse(bool success, HttpStatusCode statusCode, string message, IEnumerable<RetrieveMailingGroupDto> mailingGroups)
            : base(success, statusCode, message)
        {
            MailingGroups = mailingGroups;
        }

        public IEnumerable<RetrieveMailingGroupDto> MailingGroups { get; set; }
    }
}
