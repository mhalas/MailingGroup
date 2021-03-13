using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class DeleteEmailAddressRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public DeleteEmailAddressRequest(int mailId)
        {
            MailId = mailId;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailId { get; }
    }
}
