

using System.Net.Http.Json;
using System.Text.Json;
using Domain.Infrastructure;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public class LocationServices : ILocationService
    {
        private readonly HttpClient _client;
        private readonly string locationIqKey;

        public LocationServices(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this._client = httpClientFactory.CreateClient(InfrastructureConstants.LocationIqHttpClientName);
            this.locationIqKey = configuration.GetConnectionString(InfrastructureConstants.LocationIqKey)!;
        }

        public async Task<List<LocationIQ>?> AutoCompleteAsync(string q, int limit = 3, string format = "json",
        CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this._client.GetFromJsonAsync<List<LocationIQ>>($"autocomplete?key={locationIqKey}&q={q}&limit={limit}&format={format}",
new JsonSerializerOptions(JsonSerializerDefaults.Web), cancellationToken);
                return result;
            }
            catch
            {
                return null;
            }

        }

        public async Task<LocationIQ?> ReverseGeoCoding(double lat, double lon,
            string format = "json", CancellationToken cancellationToken = default)
        {
            var result = await this._client.GetFromJsonAsync<LocationIQ>($"reverse?key={locationIqKey}&lat={lat}&lon={lon}&format={format}", cancellationToken);
            return result;
        }
    }
}