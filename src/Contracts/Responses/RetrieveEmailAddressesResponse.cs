using Contracts.Dto;
using System.Collections.Generic;
using System.Net;

namespace Contracts.Responses
{
    public class RetrieveEmailAddressesResponse : BasicResponseInfo
    {
        public RetrieveEmailAddressesResponse(bool success, HttpStatusCode statusCode, string message, IEnumerable<RetrieveEmailAddressDto> emailAddresses) 
            : base(success, statusCode, message)
        {
            EmailAddresses = emailAddresses;
        }

        public IEnumerable<RetrieveEmailAddressDto> EmailAddresses { get; set; }
    }
}
