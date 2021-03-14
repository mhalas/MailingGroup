using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Api.CustomRequests
{
    public class UpdateEmailAddressDto
    {
        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Address { get; set; }
    }
}
