using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class UpdateMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public UpdateMailingGroupRequest(int mailingGroupId, string name)
        {
            MailingGroupId = mailingGroupId;
            Name = name;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailingGroupId { get; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; }
    }
}
