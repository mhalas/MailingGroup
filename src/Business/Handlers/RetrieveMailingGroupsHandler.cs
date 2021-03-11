using Business.Requests;
using Contracts.Dto;
using Contracts.Models;
using Contracts.Responses;
using EF.SqlServer.Models;
using MediatR;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class RetrieveMailingGroupsHandler : IRequestHandler<RetrieveMailingGroupsRequest, RetrieveMailingGroupsResponse>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveMailingGroupsHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<RetrieveMailingGroupsResponse> Handle(RetrieveMailingGroupsRequest request, CancellationToken cancellationToken)
        {
            var result = _databaseContext
            .MailingGroups
            .Where(x => x.SystemUserId == request.UserId)
            .ToList()
            .Select(x => new RetrieveMailingGroupDto(x.Id, x.Name))
            .AsEnumerable();

            return Task.FromResult(new RetrieveMailingGroupsResponse(true, HttpStatusCode.OK, string.Empty, result));
        }
    }
}
