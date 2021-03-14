using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveMailingGroupsRequest : BaseRequest, IRequest<RetrieveMailingGroupsResponse>
    {

    }
}
