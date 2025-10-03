

using Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection sc)
        {
            sc.AddScoped<IUserAccessor, UserAccessor>();
            sc.AddSingleton<IImageRepository, BlobRepository>();
            return sc;
        }
    }
}