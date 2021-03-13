using Contracts.Dto;
using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Business.Requests
{
    public class RetrieveMailsRequest : BaseRequest, IRequest<RetrieveEmailAddressesResponse>
    {
        public RetrieveMailsRequest(int? mailingGroupId = null)
        {
            MailingGroupId = mailingGroupId;
        }


        [JsonProperty(Required = Required.Default)]
        public int? MailingGroupId { get; }
    }
}
