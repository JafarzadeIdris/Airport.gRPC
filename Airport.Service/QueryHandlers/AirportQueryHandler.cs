using Airport.Provider.Provider;
using Airport.Provider.Repository;
using Airport.Service.Extensions;
using Airport.Service.Queries;
using Common.Core.Queries;

namespace Airport.Service.Commands
{
    public class AirportQueryHandler : IQueryHandler<AirportRequest, AirportResponse>
    {
        private readonly ICachedRepository _cachedRepository;

        public AirportQueryHandler(ICachedRepository cachedRepository) => _cachedRepository = cachedRepository;

        public async Task<AirportResponse> Handle(AirportRequest request, CancellationToken cancellationToken)
        {
            var firstAirportResponse = await _cachedRepository.GetAsync(request.FirstAirportIATA, cancellationToken);
            var secondAirportResponse = await _cachedRepository.GetAsync(request.SecondAirportIATA, cancellationToken);

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
