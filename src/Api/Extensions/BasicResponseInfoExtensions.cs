using Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Extensions
{
    public static class BasicResponseInfoExtensions
    {
        public static IActionResult GetResult(this BasicResponseInfo response)
        {
            if (response.StatusCode == HttpStatusCode.Conflict)
                return new ConflictObjectResult(response.Message);

            if (response.StatusCode == HttpStatusCode.Created)
                return new CreatedResult(string.Empty, response.Message);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return new NotFoundObjectResult(response.Message);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new UnauthorizedObjectResult(response.Message);

            if (response.StatusCode == HttpStatusCode.OK)
                return new OkObjectResult(response);

            return new BadRequestResult();
        }
    }
}
