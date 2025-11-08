
using Api.Controllers;
using Api.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public static class ApiRegistration
    {
        public static IServiceCollection AddApi(this IServiceCollection collection)
        {
            collection.AddControllers(options =>
            {
                var pb = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.Filters.Add(new AuthorizeFilter(pb));
            })
            .AddApplicationPart(typeof(HomeController).Assembly);
            collection.AddSignalR();
            return collection;
        }

        public static IEndpointRouteBuilder MapApiRoutes(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute("default", "api/{controller}/{action}/{id?}");
            endpointRouteBuilder.MapHub<CommentHub>("/comments");
            return endpointRouteBuilder;
        }
    }
}