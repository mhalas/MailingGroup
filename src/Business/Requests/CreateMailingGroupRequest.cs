using Contracts.Responses;
using MediatR;

namespace Business.Requests
{
    public class CreateMailingGroupRequest : BaseRequest, IRequest<BasicResponseInfo>
    {
        public CreateMailingGroupRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }

        
    }
}
