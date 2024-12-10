using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Airport.Provider.Models;
using Airport.Provider.Provider;
using Microsoft.Extensions.Caching.Distributed;

namespace Airport.Provider.Repository
{
    public class CachedRepository(IDistributedCache distributedCache, IAirportProvider airportProvider) : ICachedRepository
    {
        public async Task<AirportModel> GetAsync(string key, CancellationToken cancellationToken)
        {
            var cached = await distributedCache.GetStringAsync(key, cancellationToken);
            if (!string.IsNullOrWhiteSpace(cached))
            {
                return JsonSerializer.Deserialize<AirportModel>(cached)!;
            }
            var response = await airportProvider.GetAirportInfoAsync(key, cancellationToken);
            await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(response), cancellationToken);
            return response;
        }

    }
}
