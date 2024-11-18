using Airport.Provider.Models;


namespace Airport.Provider.Repository
{
    public interface ICachedRepository
    {
        Task<AirportModel> GetAsync(string key, CancellationToken cancellationToken);
    }
}
    