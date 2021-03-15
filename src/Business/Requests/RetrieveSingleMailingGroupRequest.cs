using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveSingleMailingGroupRequest : BaseRequest, IRequest<RetrieveSingleMailingGroupResponse>
    {
        public RetrieveSingleMailingGroupRequest(int mailingGroupId)
        {
            MailingGroupId = mailingGroupId;
        }

        public int MailingGroupId { get; }
    }
}
