using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class LoginUserRequest: IRequest<LoginUserResponse>
    {
        public LoginUserRequest(string username, string password)
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
