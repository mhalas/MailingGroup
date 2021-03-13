using Business.Requests;
using Contracts.Responses;
using EF.SqlServer.Models;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class DeleteEmailAddressHandler : IRequestHandler<DeleteEmailAddressRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;

        public DeleteEmailAddressHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BasicResponseInfo> Handle(DeleteEmailAddressRequest request, CancellationToken cancellationToken)
        {
            if(request.MailId == default(int))
                return new BasicResponseInfo(false, HttpStatusCode.BadRequest, "Required email address.");

            var emailAddressToDelete = _databaseContext
                .EmailAddress
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailId)
                .SingleOrDefault();

            if (emailAddressToDelete == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Email address not found.");

            _databaseContext.Remove(emailAddressToDelete);
            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Email address deleted.");
        }
    }
}
