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
    public class RetrieveMailsHandler : IRequestHandler<RetrieveMailsRequest, RetrieveEmailAddressesResponse>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveMailsHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<RetrieveEmailAddressesResponse> Handle(RetrieveMailsRequest request, CancellationToken cancellationToken)
        {
            var query = _databaseContext
                .Mail
                .Where(x => x.MailingGroup.SystemUserId == request.UserId);

            if (request.MailingGroupId.HasValue)
                query.Where(x => x.MailingGroupId == request.MailingGroupId);

            var results = query
                .ToList()
                .Select(x => new RetrieveEmailAddressDto(x.Id, x.MailingGroupId, x.Address));

            return Task.FromResult(new RetrieveEmailAddressesResponse(true, 
                HttpStatusCode.OK, 
                string.Empty, 
                results));
        }
    }
}
