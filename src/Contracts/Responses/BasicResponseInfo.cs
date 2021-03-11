using Newtonsoft.Json;
using System.Net;

namespace Contracts.Responses
{
    [JsonObject]
    public class BasicResponseInfo
    {
        public BasicResponseInfo(bool success, HttpStatusCode statusCode, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

        [JsonIgnore]
        public bool Success { get; }
        
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; }
    }
}
