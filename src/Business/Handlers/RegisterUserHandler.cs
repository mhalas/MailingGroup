using Business.Requests;
using Contracts.Responses;
using EF.SqlServer.Models;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace Business.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UserPasswordUtility _userPasswordUtility;
        private readonly SaltGeneratorUtility _saltGeneratorUtility;

        public RegisterUserHandler(DatabaseContext databaseContext, 
            UserPasswordUtility userPasswordUtility, 
            SaltGeneratorUtility saltGeneratorUtility)
        {
            _databaseContext = databaseContext;
            _userPasswordUtility = userPasswordUtility;
            _saltGeneratorUtility = saltGeneratorUtility;
        }

        public async Task<BasicResponseInfo> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
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
            await _databaseContext.SaveChangesAsync();

            return new BasicResponseInfo(true, HttpStatusCode.Created, "User added.");
        }
    }
}
