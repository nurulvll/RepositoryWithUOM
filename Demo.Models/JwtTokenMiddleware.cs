using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {

            if (context.Request.Query.ContainsKey("token"))
            {
                var token = context.Request.Query["token"].ToString();
                context.Request.Headers["Authorization"] = "Bearer " + token;
            }


            await _next(context);
        }
    }
}
