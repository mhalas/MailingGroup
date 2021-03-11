using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class RegisterUserRequest : IRequest<BasicResponseInfo>
    {
        public RegisterUserRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonProperty(Required = Required.Always)]
        public string Username { get; }

        [JsonProperty(Required = Required.Always)]
        public string Password { get; }
    }
}
