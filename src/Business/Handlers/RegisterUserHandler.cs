using Business.Requests;
using Contracts.Responses;
using Contracts.Utility;
using EF.SqlServer.Models;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IUserPasswordUtility _userPasswordUtility;
        private readonly ISaltGeneratorUtility _saltGeneratorUtility;

        public RegisterUserHandler(DatabaseContext databaseContext, 
            IUserPasswordUtility userPasswordUtility, 
            ISaltGeneratorUtility saltGeneratorUtility)
        {
            _databaseContext = databaseContext;
            _userPasswordUtility = userPasswordUtility;
            _saltGeneratorUtility = saltGeneratorUtility;
        }

        public async Task<BasicResponseInfo> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return new BasicResponseInfo(false, HttpStatusCode.BadRequest, "Required 'Username' and 'Password'.");

            var isUserExist = _databaseContext.SystemUsers.Any(x => x.Username == request.Username);
            if(isUserExist)
                return new BasicResponseInfo(false, HttpStatusCode.Conflict, "User already exists!");

            var userSalt = _saltGeneratorUtility.Generate();

            var password = _userPasswordUtility.HashPassword(request.Password, userSalt);

            var newUser = new SystemUser()
            {
                Username = request.Username,
                Salt = userSalt,
                Password = password
            };

            await _databaseContext.AddAsync(newUser).ConfigureAwait(false);
            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

            return new BasicResponseInfo(true, HttpStatusCode.Created, "User added.");
        }
    }
}
