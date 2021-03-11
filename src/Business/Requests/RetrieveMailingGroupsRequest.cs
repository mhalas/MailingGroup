using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class RetrieveMailingGroupsRequest: IRequest<RetrieveMailingGroupsResponse>
    {
        public int UserId { get; set; }
    }
}
