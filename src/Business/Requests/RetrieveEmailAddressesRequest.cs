using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveEmailAddressesRequest : BaseRequest, IRequest<RetrieveEmailAddressesResponse>
    {
        public RetrieveEmailAddressesRequest(int? mailingGroupId)
        {
            MailingGroupId = mailingGroupId;
        }

        public int? MailingGroupId { get; set; }
    }
}
