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
    public class CreateMailingGroupHandler : IRequestHandler<CreateMailingGroupRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;

        public CreateMailingGroupHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<BasicResponseInfo> Handle(CreateMailingGroupRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Name))
                return new BasicResponseInfo(false,
                    HttpStatusCode.BadRequest,
                    $"Mailing group name is undefined.");

            var existingMailingGroup = _databaseContext
                .MailingGroups
                .Where(x => x.SystemUserId == request.UserId)
                .Any(x => x.Name == request.Name);

            if (existingMailingGroup)
                return new BasicResponseInfo(false,
                    HttpStatusCode.Conflict,
                    $"The operation has been rolled back. Mailing group name '{request.Name}' has already added.");

            var newMailingGroup = new MailingGroup()
            {
                Name = request.Name,
                SystemUserId = request.UserId
            };

            await _databaseContext.AddAsync(newMailingGroup).ConfigureAwait(false);
            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);

            return new BasicResponseInfo(true,
                HttpStatusCode.Created,
                $"Success. Mailing group with name '{request.Name}' added.");
        }
    }
}
