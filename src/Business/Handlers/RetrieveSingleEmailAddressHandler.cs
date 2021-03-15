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
    public class RetrieveSingleEmailAddressHandler : IRequestHandler<RetrieveSingleEmailAddressRequest, RetrieveSingleEmailAddressResponse>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveSingleEmailAddressHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<RetrieveSingleEmailAddressResponse> Handle(RetrieveSingleEmailAddressRequest request, CancellationToken cancellationToken)
        {
            var result = _databaseContext
            .EmailAddress
            .Where(x => x.MailingGroup.SystemUserId == request.UserId)
            .Where(x => x.Id == request.EmailAddressId)
            .ToList()
            .Select(x => new RetrieveEmailAddressDto(x.Id, x.MailingGroupId, x.Value))
            .SingleOrDefault();

            if(result == null)
                return Task.FromResult(new RetrieveSingleEmailAddressResponse(false,
                    HttpStatusCode.NotFound,
                    "Email address not found",
                    result));

            return Task.FromResult(new RetrieveSingleEmailAddressResponse(true,
                HttpStatusCode.OK,
                null,
                result));
        }
    }
}
