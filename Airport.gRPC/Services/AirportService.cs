using Airport.Service.Queries;
using Mapster;
using MediatR;

namespace Airport.gRPC.Services
{

    public class AirportService : Airport.AirportBase
    {
        private readonly ISender _sender;
        public AirportService(ISender sender)
        {
            _sender = sender;
        }
        public override async Task<AirportReponse> GetDistanceBetweenAirportsByIATA(GetAirportRequest request, ServerCallContext context)
        {
            try
            {

                AirportRequest airportRequest = request.BuildAdapter().AdaptToType<AirportRequest>();

                var response = await _sender.Send(airportRequest);

                AirportReponse airportResponse = response.Adapt<AirportReponse>();

                return await Task.FromResult(airportResponse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
