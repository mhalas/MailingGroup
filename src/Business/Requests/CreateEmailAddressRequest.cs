using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.Requests
{
    public class CreateEmailAddressRequest : IRequest<BasicResponseInfo>
    {

        public CreateEmailAddressRequest(int mailingGroupId, IEnumerable<string> addresses)
        {
            MailingGroupId = mailingGroupId;
            Addresses = addresses;
        }

        public int MailingGroupId { get; }

        [JsonProperty(Required = Required.Always)]
        public IEnumerable<string> Addresses { get; }
    }
}
