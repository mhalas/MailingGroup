using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class UpdateEmailAddressRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public UpdateEmailAddressRequest(int mailId, string address)
        {
            MailId = mailId;
            Address = address;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailId { get; }

        [JsonProperty(Required = Required.Always)]
        public string Address { get; }
    }
}
