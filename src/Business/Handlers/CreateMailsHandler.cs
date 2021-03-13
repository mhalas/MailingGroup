using Contracts.Responses;
using Business.Requests;
using EF.SqlServer.Models;
using Utilities;
using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class CreateMailsHandler : IRequestHandler<CreateMailsRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly EmailAddressValidatorUtility _emailAddressValidatorUtility;

        public CreateMailsHandler(DatabaseContext databaseContext,
            EmailAddressValidatorUtility mailValidatorUtility)
        {
            _databaseContext = databaseContext;
            _emailAddressValidatorUtility = mailValidatorUtility;
        }

        public async Task<BasicResponseInfo> Handle(CreateMailsRequest request, CancellationToken cancellationToken)
        {
            var addressesNotValid = request.Addresses.Where(x => !_emailAddressValidatorUtility.ValidateMail(x));
            if (addressesNotValid.Any())
                return new BasicResponseInfo(false,
                    HttpStatusCode.BadRequest,
                    $"The operation has been rolled back. Invalid email addresses: {string.Join(", ", addressesNotValid)}.");

            var addressesAlreadyAdded = _databaseContext
                .Mail
                .Where(x => x.MailingGroupId == request.MailingGroupId)
                .Where(x => request.Addresses.Contains(x.Address))
                .Select(x=>x.Address)
                .AsEnumerable();

            if (addressesAlreadyAdded.Any())
                return new BasicResponseInfo(false,
                    HttpStatusCode.Conflict,
                    $"The operation has been rolled back. Addresses already added: {string.Join(", ", addressesAlreadyAdded)}.");

            var addressesToAdd = request
                .Addresses
                .Except(addressesAlreadyAdded)
                .Select(emailAddress => new Mail()
            {
                MailingGroupId = request.MailingGroupId,
                Address = emailAddress,
            });

            await _databaseContext.AddRangeAsync(addressesToAdd);
            await _databaseContext.SaveChangesAsync();

            if (addressesAlreadyAdded.Any())
                return new BasicResponseInfo(true, HttpStatusCode.Created, $"Success. Added addresses: {string.Join(", ", addressesToAdd)}, and ignored already added addresses: {string.Join(", ", addressesAlreadyAdded)}");

            return new BasicResponseInfo(true, HttpStatusCode.Created, $"Success.");
        }
    }
}
