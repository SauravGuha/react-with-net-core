

using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Api.Middlewares
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public ExceptionMiddleware(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/xml";
                if (webHostEnvironment.EnvironmentName.ToLower() != "development")
                {
                    await context.Response.WriteAsync(ex.Message);
                }
                else
                {
                    await context.Response.WriteAsync(ex.ToString());
                }
            }
        }
    }
}