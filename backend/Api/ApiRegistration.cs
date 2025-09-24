
using Api.Controllers;
using Microsoft.AspNetCore.Builder;
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
            return collection;
        }

        public static IEndpointRouteBuilder AddApiRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
            return endpointRouteBuilder;
        }
    }
}