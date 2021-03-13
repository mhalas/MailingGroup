using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class CreateMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public CreateMailingGroupRequest(string name)
        {
            Name = name;
        }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; }
    }
}
