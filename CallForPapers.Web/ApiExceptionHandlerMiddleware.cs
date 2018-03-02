using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CallForPapers.Web
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ApiExceptionHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = 500;
                context.Response.Headers.Add("content-type", "application/json");
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new
                {
                    error = new
                    {
                        message = ex.Message,
                        exception = ex
                    }
                }));
            }
        }
    }
}