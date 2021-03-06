using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class CreateMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public CreateMailingGroupRequest(string name)
        {
            Name = name;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
