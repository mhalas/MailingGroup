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
    public class DeleteMailHandler : IRequestHandler<DeleteMailRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;

        public DeleteMailHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BasicResponseInfo> Handle(DeleteMailRequest request, CancellationToken cancellationToken)
        {
            var mailToDelete = _databaseContext
                .Mail
                .Where(x => x.MailingGroup.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailId)
                .SingleOrDefault();

            if (mailToDelete == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Email address not found.");

            _databaseContext.Mail.Remove(mailToDelete);
            await _databaseContext.SaveChangesAsync();

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Email address deleted.");
        }
    }
}
