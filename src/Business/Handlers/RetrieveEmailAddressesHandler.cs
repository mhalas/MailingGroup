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
    public class RetrieveEmailAddressesHandler : IRequestHandler<RetrieveEmailAddressesRequest, RetrieveEmailAddressesResponse>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveEmailAddressesHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<RetrieveEmailAddressesResponse> Handle(RetrieveEmailAddressesRequest request, CancellationToken cancellationToken)
        {
            var query = _databaseContext
                .EmailAddress
                .Where(x => x.MailingGroup.SystemUserId == request.UserId);

            if (request.MailingGroupId.HasValue)
                query = query.Where(x => x.MailingGroupId == request.MailingGroupId.Value);

            var results = query
                .ToList()
                .Select(x => new RetrieveEmailAddressDto(x.Id, x.MailingGroupId, x.Value));

            return Task.FromResult(new RetrieveEmailAddressesResponse(true, 
                HttpStatusCode.OK,
                results));
        }
    }
}
