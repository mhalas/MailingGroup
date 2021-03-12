using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class DeleteMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public DeleteMailingGroupRequest(int mailingGroupId)
        {
            MailingGroupId = mailingGroupId;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailingGroupId { get; }
    }
}
