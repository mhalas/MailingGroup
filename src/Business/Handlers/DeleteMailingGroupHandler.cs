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
    public class DeleteMailingGroupHandler : IRequestHandler<DeleteMailingGroupRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;

        public DeleteMailingGroupHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BasicResponseInfo> Handle(DeleteMailingGroupRequest request, CancellationToken cancellationToken)
        {
            var mailingGroupToDelete = _databaseContext
                .MailingGroups
                .Where(x => x.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailingGroupId)
                .SingleOrDefault();

            if (mailingGroupToDelete == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Mailing group not found.");

            _databaseContext.Remove(mailingGroupToDelete);
            await _databaseContext.SaveChangesAsync();

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Mailing group deleted.");
        }
    }
}
