using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Api.CustomRequests
{
    public class UpdateMailingGroupDto
    {
        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}
