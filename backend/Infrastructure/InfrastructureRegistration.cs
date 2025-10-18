

using Domain.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection sc)
        {
            sc.AddScoped<IUserAccessor, UserAccessor>();
            return sc;
        }
    }
}