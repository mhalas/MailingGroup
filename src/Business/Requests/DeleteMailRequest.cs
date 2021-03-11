﻿using Contracts.Responses;
using MediatR;
using Newtonsoft.Json;

namespace Business.Requests
{
    public class DeleteMailRequest : IRequest<BasicResponseInfo>
    {
        public DeleteMailRequest(int mailId)
        {
            MailId = mailId;
        }

        [JsonProperty(Required = Required.Always)]
        public int MailId { get; }

        public int UserId { get; set; }
    }
}