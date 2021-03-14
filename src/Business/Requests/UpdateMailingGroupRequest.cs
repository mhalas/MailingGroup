using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class UpdateMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public UpdateMailingGroupRequest(int mailingGroupId, string name)
        {
            MailingGroupId = mailingGroupId;
            Name = name;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public int MailingGroupId { get; set; }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
