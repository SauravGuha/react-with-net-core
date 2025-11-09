

using Domain.Models;

namespace Domain.Infrastructure
{
    public interface ILocationService
    {
        Task<List<LocationIQ>?> AutoCompleteAsync(string q, int limit = 3, string format = "json",
         CancellationToken cancellationToken = default);

        Task<LocationIQ?> ReverseGeoCoding(double lat, double lon, string format = "json",
         CancellationToken cancellationToken = default);
    }
}