using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static int? GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User.HasClaim(x => x.Type == "UserId") &&
                int.TryParse(httpContext.User.Claims.First(x => x.Type == "UserId").Value, out int userId))
            {
                return userId;
            }

            return null;
        }
    }
}
