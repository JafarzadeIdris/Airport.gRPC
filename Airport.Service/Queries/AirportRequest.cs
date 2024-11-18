using Common.Core.Queries;

namespace Airport.Service.Queries;

public record AirportRequest : IQuery<AirportResponse>
{
    public string FirstAirportIATA { get; set; }
    public string SecondAirportIATA { set; get; }
}

