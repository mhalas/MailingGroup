using Business.Requests;
using Contracts.Responses;
using Contracts.Utility;
using EF.SqlServer.Models;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUserPasswordUtility _userPasswordUtility;
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public LoginUserHandler(DatabaseContext databaseContext,
            IUserPasswordUtility userPasswordUtility,
            IConfiguration configuration,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _databaseContext = databaseContext;
            _userPasswordUtility = userPasswordUtility;
            _configuration = configuration;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }


        public Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var loggingUser = _databaseContext.SystemUsers.SingleOrDefault(x => x.Username == request.Username);
            if (loggingUser == null)
                return Task.FromResult(new LoginUserResponse(false, HttpStatusCode.NotFound, "User not found."));

            if(!_userPasswordUtility.IsPasswordCorrect(request.Password, loggingUser.Salt, loggingUser.Password))
                return Task.FromResult(new LoginUserResponse(false, HttpStatusCode.Unauthorized, "Password incorrect."));

            var claims = new[] {
                new Claim("UserId", loggingUser.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            return Task.FromResult(new LoginUserResponse(_jwtSecurityTokenHandler.WriteToken(token), token.ValidTo, true, HttpStatusCode.OK, string.Empty));
        }
    }
}
