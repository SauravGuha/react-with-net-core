

using Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection sc, IConfiguration configuration)
        {
            if (string.IsNullOrWhiteSpace(configuration.GetConnectionString(InfrastructureConstants.LocationIqBaseurl)))
            {
                throw new ArgumentNullException($"{InfrastructureConstants.LocationIqBaseurl} is empty");
            }

            if (string.IsNullOrWhiteSpace(configuration.GetConnectionString(InfrastructureConstants.LocationIqKey)))
            {
                throw new ArgumentNullException($"{InfrastructureConstants.LocationIqKey} is empty");
            }
            sc.AddScoped<IUserAccessor, UserAccessor>();
            sc.AddSingleton<IImageRepository, BlobRepository>();
            sc.AddSingleton<ILocationService, LocationServices>();
            sc.AddSingleton<IEmailSenderService, EmailSenderService>();
            sc.AddHttpClient(InfrastructureConstants.LocationIqHttpClientName, client =>
            {
                client.BaseAddress = new Uri(configuration.GetConnectionString(InfrastructureConstants.LocationIqBaseurl)!);
            });
            return sc;
        }
    }
}