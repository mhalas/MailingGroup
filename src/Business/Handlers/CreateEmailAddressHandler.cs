using Business.Requests;
using Contracts.Responses;
using Contracts.Utility;
using EF.SqlServer.Models;
using MediatR;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers
{
    public class CreateEmailAddressHandler : IRequestHandler<CreateEmailAddressRequest, BasicResponseInfo>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IEmailAddressValidatorUtility _emailAddressValidatorUtility;

        public CreateEmailAddressHandler(DatabaseContext databaseContext,
            IEmailAddressValidatorUtility mailValidatorUtility)
        {
            _databaseContext = databaseContext;
            _emailAddressValidatorUtility = mailValidatorUtility;
        }

        public async Task<BasicResponseInfo> Handle(CreateEmailAddressRequest request, CancellationToken cancellationToken)
        {
            if(!request.Addresses.Any() || request.Addresses.Any(x => string.IsNullOrEmpty(x)) || request.MailingGroupId == default(int))
                return new BasicResponseInfo(false,
                    HttpStatusCode.BadRequest,
                    $"Required Addresses and MailingGroupId.");

            var addressesNotValid = request.Addresses.Where(x => !_emailAddressValidatorUtility.ValidateMail(x));
            if (addressesNotValid.Any())
                return new BasicResponseInfo(false,
                    HttpStatusCode.BadRequest,
                    $"The operation has been rolled back. Invalid email addresses: {string.Join(", ", addressesNotValid)}.");

            var addressesAlreadyAdded = _databaseContext
                .EmailAddress
                .Where(x => x.MailingGroupId == request.MailingGroupId)
                .Where(x => request.Addresses.Contains(x.Value))
                .Select(x=>x.Value)
                .AsEnumerable();

            if (addressesAlreadyAdded.Any())
                return new BasicResponseInfo(false,
                    HttpStatusCode.Conflict,
                    $"The operation has been rolled back. Addresses already added: {string.Join(", ", addressesAlreadyAdded)}.");

            var addressesToAdd = request
                .Addresses
                .Select(emailAddress => new EmailAddress()
                {
                    MailingGroupId = request.MailingGroupId,
                    Value = emailAddress,
                })
                .ToList();

            foreach (var address in addressesToAdd)
                await _databaseContext.AddAsync(address).ConfigureAwait(false);

            await _databaseContext.SaveChangesAsync().ConfigureAwait(false);
            return new BasicResponseInfo(true, HttpStatusCode.Created, $"Success.");
        }
    }
}
