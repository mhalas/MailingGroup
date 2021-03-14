using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class DeleteMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public DeleteMailingGroupRequest(int mailingGroupId)
        {
            MailingGroupId = mailingGroupId;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public int MailingGroupId { get; set; }
    }
}
