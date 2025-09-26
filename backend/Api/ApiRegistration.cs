
using Api.Controllers;
using Api.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApi(this IServiceCollection collection)
        {
            collection.AddControllers()
            .AddApplicationPart(typeof(HomeController).Assembly);
            collection.AddScoped<ExceptionMiddleware>();
            return collection;
        }

        public static IEndpointRouteBuilder MapApiRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
            return endpointRouteBuilder;
        }

        public static IApplicationBuilder UseException(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionMiddleware>();
            return applicationBuilder;
        }
    }
}