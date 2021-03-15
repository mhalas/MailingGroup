using Business.Requests;
using Contracts.Dto;
using Contracts.Responses;
using EF.SqlServer.Models;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class RetrieveSingleMailingGroupHandler : IRequestHandler<RetrieveSingleMailingGroupRequest, RetrieveSingleMailingGroupResponse>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveSingleMailingGroupHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<RetrieveSingleMailingGroupResponse> Handle(RetrieveSingleMailingGroupRequest request, CancellationToken cancellationToken)
        {
            var result = _databaseContext
            .MailingGroups
            .Where(x => x.SystemUserId == request.UserId)
            .Where(x => x.Id == request.MailingGroupId)
            .ToList()
            .Select(x => new RetrieveMailingGroupDto(x.Id, x.Name))
            .SingleOrDefault();

            if (result == null)
                return Task.FromResult(new RetrieveSingleMailingGroupResponse(false,
                    HttpStatusCode.NotFound,
                    "Mailing group not found.",
                    result));

            return Task.FromResult(new RetrieveSingleMailingGroupResponse(true,
                HttpStatusCode.OK,
                null,
                result));
        }
    }
}
