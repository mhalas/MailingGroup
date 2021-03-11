using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class UpdateMailRequest : IRequest<BasicResponseInfo>
    {
        public UpdateMailRequest(int mailId, string address)
        {
            MailId = mailId;
            Address = address;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailId { get; }

        [JsonProperty(Required = Required.Always)]
        public string Address { get; }

        public int UserId { get; set; }
    }
}
