using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveMailsRequest : BaseRequest, IRequest<RetrieveEmailAddressesResponse>
    {
        public RetrieveMailsRequest(int? mailingGroupId)
        {
            MailingGroupId = mailingGroupId;
        }

        public int? MailingGroupId { get; set; }
    }
}
