using Airport.Provider.Provider;
using Airport.Service.Extensions;
using Airport.Service.Queries;
using Common.Core.Queries;

namespace Airport.Service.Commands
{
    public class AirportQueryHandler : IQueryHandler<AirportRequest, AirportResponse>
    {
        private readonly IAirportProvider _airportProvider;

        public AirportQueryHandler(IAirportProvider airportProvider) => _airportProvider = airportProvider;

        public async Task<AirportResponse> Handle(AirportRequest request, CancellationToken cancellationToken)
        {
            var firstAirportResponse = await _airportProvider.GetAirportInfoAsync(request.FirstAirportIATA, cancellationToken);
            var secondAirportResponse = await _airportProvider.GetAirportInfoAsync(request.SecondAirportIATA, cancellationToken);

            var firstLocation = (firstAirportResponse.Location!.Lat, firstAirportResponse.Location.Lon);

            var secondLocation = (secondAirportResponse.Location!.Lat, secondAirportResponse.Location.Lon);

            double distance = firstLocation.CalculateDistance(secondLocation);

            AirportResponse airportResponses = new(
                FirstAirportName: firstAirportResponse.Name!, 
                SecondAirportName: secondAirportResponse.Name!, 
                Distance: distance);

            return await Task.FromResult(airportResponses);
        }
    }
}
