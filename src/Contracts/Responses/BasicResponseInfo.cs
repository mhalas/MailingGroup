#nullable enable
using Newtonsoft.Json;
using System.Net;

namespace Contracts.Responses
{
    [JsonObject]
    public class BasicResponseInfo
    {
        public BasicResponseInfo(bool success, HttpStatusCode statusCode, string? message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

        public BasicResponseInfo(bool success, HttpStatusCode statusCode)
        {
            Success = success;
            StatusCode = statusCode;
        }

        [JsonIgnore]
        public bool Success { get; }
        
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; }
    }
}
