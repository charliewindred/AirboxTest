using AirboxTechTest.Models;

namespace AirboxTechTest.Repositories
{
    public interface ILocationRepository
    {
        public Task<Location?> GetMostRecentLocationByUserIdAsync(int id);
        public Task<IList<Location>> GetMostRecentLocationForAllUsersAsync();
        public Location AddLocation(Location location);
        public Task<IList<Location>> GetAllLocationsByUserIdAsync(int id);
    }
}
