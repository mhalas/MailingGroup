using System;
using System.Net;

namespace Contracts.Responses
{
    public class LoginUserResponse : BasicResponseInfo
    {
        public LoginUserResponse(bool success, HttpStatusCode statusCode, string error)
            : base(success, statusCode, error)
        {

        }

        public LoginUserResponse(string token, DateTime expires, bool success, HttpStatusCode statusCode, string error)
            : base(success, statusCode, error)
        {
            Token = token;
            Expires = expires;
        }

        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
