using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Authentication
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //public async Task Invoke(HttpContext context)
        //{
        //    var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        //    if (token == "1")
        //        context.Items["User"] = int.Parse(token);


        //    await _next(context);
        //}
    }
}
