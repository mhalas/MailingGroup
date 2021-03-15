using Contracts.Dto;
using System.Net;

namespace Contracts.Responses
{
    public class RetrieveSingleEmailAddressResponse: BasicResponseInfo
    {
        public RetrieveSingleEmailAddressResponse(bool success, HttpStatusCode statusCode, string message, RetrieveEmailAddressDto emailAddress) 
            : base(success, statusCode, message)
        {
            EmailAddress = emailAddress;
        }

        public RetrieveEmailAddressDto EmailAddress { get; }
    }
}
