

using Application.Activities.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection sc)
        {
            sc.AddAutoMapper(cfg => { }, typeof(ActivityQueryRequest).Assembly);
            sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ActivityQueryRequest).Assembly));
            return sc;
        }
    }
}