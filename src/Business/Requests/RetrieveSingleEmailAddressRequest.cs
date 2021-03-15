using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveSingleEmailAddressRequest : BaseRequest, IRequest<RetrieveSingleEmailAddressResponse>
    {
        public RetrieveSingleEmailAddressRequest(int emailAddressId)
        {
            EmailAddressId = emailAddressId;
        }

        public int EmailAddressId { get; }
    }
}
