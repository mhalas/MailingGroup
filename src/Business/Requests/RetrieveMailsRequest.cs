using Contracts.Models;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.Requests
{
    public class RetrieveMailsRequest : BaseRequest, IRequest<IEnumerable<IMail>>
    {
        public RetrieveMailsRequest(int? mailingGroupId = null)
        {
            MailingGroupId = mailingGroupId;
        }


        [JsonProperty(Required = Required.Default)]
        public int? MailingGroupId { get; }
    }
}
