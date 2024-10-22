using Common.Core.Queries;

namespace Airport.Service.Queries;

public record AirportRequest(string FirstAirportIATA, string SecondAirportIATA) : IQuery<AirportResponse>{}
