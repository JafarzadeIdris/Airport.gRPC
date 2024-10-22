using Airport.Provider.Models;
using System.Net.Http.Json;

namespace Airport.Provider.Provider
{
    public class AirportProvider : IAirportProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AirportProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AirportModel> GetAirportInfoAsync(string airportIATA, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(airportIATA) || airportIATA.Length != 3)
            {
                throw new ArgumentException($"Invalid IATA code. Expected 3 characters, but got '{airportIATA}'.");
            }
            var client = _httpClientFactory.CreateClient();

            using var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://places-dev.cteleport.com/airports/{airportIATA}");

            using var httpResponseMessage = await client.SendAsync(httpRequestMessage, cancellationToken);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = await httpResponseMessage.Content.ReadFromJsonAsync<AirportModel>(cancellationToken);
                return content!;
            }
            else {
                var errorContent = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
                throw new Exception($"Error: {httpResponseMessage.StatusCode}, Details: {errorContent}");
            }

        }
    }
}
