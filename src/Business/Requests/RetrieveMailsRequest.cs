using Contracts.Dto;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.Requests
{
    public class RetrieveMailsRequest : BaseRequest, IRequest<IEnumerable<RetrieveMailDto>>
    {
        public RetrieveMailsRequest(int? mailingGroupId = null)
        {
            MailingGroupId = mailingGroupId;
        }


        [JsonProperty(Required = Required.Default)]
        public int? MailingGroupId { get; }
    }
}
