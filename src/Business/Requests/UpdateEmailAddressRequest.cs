using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class UpdateEmailAddressRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public UpdateEmailAddressRequest(int emailAddressId, string address)
        {
            EmailAddressId = emailAddressId;
            Address = address;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public int EmailAddressId { get; set; }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Address { get; set; }
    }
}
