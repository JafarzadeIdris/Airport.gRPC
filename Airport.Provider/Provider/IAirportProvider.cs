using Airport.Provider.Models;
using CSharpFunctionalExtensions;

namespace Airport.Provider.Provider
{
    public interface IAirportProvider
    {
        Task<AirportModel> GetAirportInfoAsync(string airportIATA, CancellationToken cancellationToken);
    }
}
