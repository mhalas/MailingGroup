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
    public class UpdateMailHandler : IRequestHandler<UpdateMailRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly MailValidatorUtility _mailValidatorUtility;

        public UpdateMailHandler(DatabaseContext databaseContext, 
            MailValidatorUtility mailValidatorUtility)
        {
            _databaseContext = databaseContext;
            _mailValidatorUtility = mailValidatorUtility;
        }

        public async Task<BasicResponseInfo> Handle(UpdateMailRequest request, CancellationToken cancellationToken)
        {
            if (_mailValidatorUtility.ValidateMail(request.Address))
                return new BasicResponseInfo(false, HttpStatusCode.BadRequest, "Invalid email address.");

            var isAddressAlreadyExists = _databaseContext
                .Mail
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Address == request.Address)
                .Any();

            if (isAddressAlreadyExists)
                return new BasicResponseInfo(false, HttpStatusCode.Conflict, "Email address already exists.");

            var mailToUpdate = _databaseContext
                .Mail
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailId)
                .SingleOrDefault();

            if(mailToUpdate == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Email address not found.");

            mailToUpdate.Address = request.Address;
            await _databaseContext.SaveChangesAsync();

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Email address updated.");
        }
    }
}
