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
    public class UpdateEmailAddressHandler : IRequestHandler<UpdateEmailAddressRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IEmailAddressValidatorUtility _emailAddressValidatorUtility;

        public UpdateEmailAddressHandler(DatabaseContext databaseContext, 
            IEmailAddressValidatorUtility mailValidatorUtility)
        {
            _databaseContext = databaseContext;
            _emailAddressValidatorUtility = mailValidatorUtility;
        }

        public async Task<BasicResponseInfo> Handle(UpdateEmailAddressRequest request, CancellationToken cancellationToken)
        {
            if (_emailAddressValidatorUtility.ValidateMail(request.Address))
                return new BasicResponseInfo(false, HttpStatusCode.BadRequest, "Invalid email address.");

            var isAddressAlreadyExists = _databaseContext
                .EmailAddress
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Value == request.Address)
                .Any();

            if (isAddressAlreadyExists)
                return new BasicResponseInfo(false, HttpStatusCode.Conflict, "Email address already exists.");

            var emailAddressToUpdate = _databaseContext
                .EmailAddress
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailId)
                .SingleOrDefault();

            if(emailAddressToUpdate == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Email address not found.");

            emailAddressToUpdate.Value = request.Address;
            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Email address updated.");
        }
    }
}
