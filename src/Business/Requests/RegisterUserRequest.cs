using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Business.Requests
{
    public class RegisterUserRequest : IRequest<BasicResponseInfo>
    {
        public RegisterUserRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Username { get; set; }

        [Required]
        [JsonProperty(Required = Required.Always)]
        public string Password { get; set; }
    }
}
