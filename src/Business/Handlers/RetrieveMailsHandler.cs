using Business.Requests;
using Contracts.Models;
using EF.SqlServer.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class RetrieveMailsHandler : IRequestHandler<RetrieveMailsRequest, IEnumerable<IMail>>
    {
        private readonly DatabaseContext _databaseContext;

        public RetrieveMailsHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<IEnumerable<IMail>> Handle(RetrieveMailsRequest request, CancellationToken cancellationToken)
        {
            var query = _databaseContext
                .Mail
                .Where(x => x.MailingGroup.SystemUserId == request.UserId);

            if (request.MailingGroupId.HasValue)
                query.Where(x => x.MailingGroupId == request.MailingGroupId);

            return Task.FromResult(query
                .ToList()
                .AsEnumerable()
                .Cast<IMail>());
        }
    }
}
