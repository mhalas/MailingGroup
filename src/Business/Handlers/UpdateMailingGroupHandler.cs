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
    public class UpdateMailingGroupHandler : IRequestHandler<UpdateMailingGroupRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;

        public UpdateMailingGroupHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BasicResponseInfo> Handle(UpdateMailingGroupRequest request, CancellationToken cancellationToken)
        {
            var isMailingGroupWithExistingName = _databaseContext
                .MailingGroups
                .Where(x => x.SystemUserId == request.UserId)
                .Where(x => x.Name == request.Name)
                .Any();

            if (isMailingGroupWithExistingName)
                return new BasicResponseInfo(false, HttpStatusCode.Conflict, $"Mailing group with name '{request.Name}' already exists.");

            var mailingGroupToUpdate = _databaseContext
                .MailingGroups
                .Where(x => x.SystemUserId == request.UserId)
                .Where(x => x.Id == request.MailingGroupId)
                .SingleOrDefault();

            if (mailingGroupToUpdate == null)
                return new BasicResponseInfo(false, HttpStatusCode.NotFound, "Email address not found.");

            mailingGroupToUpdate.Name = request.Name;
            await _databaseContext.SaveChangesAsync();

            return new BasicResponseInfo(true, HttpStatusCode.OK, "Email address updated.");
        }
    }
}
