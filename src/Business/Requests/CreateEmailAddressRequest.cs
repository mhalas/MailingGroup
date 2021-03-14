using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Business.Requests
{
    public class CreateEmailAddressRequest : IRequest<BasicResponseInfo>
    {

        public CreateEmailAddressRequest(int mailingGroupId, IEnumerable<string> addresses)
        {
            MailingGroupId = mailingGroupId;
            Addresses = addresses;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public int MailingGroupId { get; set; }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<string> Addresses { get; set; }
    }
}
