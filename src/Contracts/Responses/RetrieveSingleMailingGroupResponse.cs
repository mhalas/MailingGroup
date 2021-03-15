using Contracts.Dto;
using System.Net;

namespace Contracts.Responses
{
    public class RetrieveSingleMailingGroupResponse : BasicResponseInfo
    {
        public RetrieveSingleMailingGroupResponse(bool success, HttpStatusCode statusCode, string message, RetrieveMailingGroupDto mailingGroup) 
            : base(success, statusCode, message)
        {
            MailingGroup = mailingGroup;
        }

        public RetrieveMailingGroupDto MailingGroup { get; }
    }
}
