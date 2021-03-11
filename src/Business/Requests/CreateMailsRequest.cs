using Contracts.Responses;
using MediatR;
using System.Collections.Generic;

namespace Business.Requests
{
    public class CreateMailsRequest : IRequest<BasicResponseInfo>
    {

        public CreateMailsRequest(int mailingGroupId, IEnumerable<string> addresses)
        {
            MailingGroupId = mailingGroupId;
            Addresses = addresses;
        }

        public int MailingGroupId { get; }

        public IEnumerable<string> Addresses { get; }
    }
}
