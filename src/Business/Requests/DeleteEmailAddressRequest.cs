using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class DeleteEmailAddressRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public DeleteEmailAddressRequest(int emailAddressId)
        {
            EmailAddressId = emailAddressId;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public int EmailAddressId { get; set; }
    }
}
