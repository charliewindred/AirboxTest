using AirboxTechTest.Context;
using AirboxTechTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AirboxTechTest.Repositories
{
    /// <remark>
    /// I included multiple ways of using the context because I wasn't sure what best practice is.
    /// Struggled to see how I could mock the context for a unit test when creating a new one for each method, so DI is my best guess.
    /// https://medium.com/@codebob75/repository-pattern-c-ultimate-guide-entity-framework-core-clean-architecture-dtos-dependency-6a8d8b444dcb
    /// https://mehdi.me/ambient-dbcontext-in-ef6/
    /// </remark>
    public class LocationRepository(LocationContext locationContext) : ILocationRepository
    {
        private readonly LocationContext _locationContext = locationContext;

        public Location AddLocation(Location location)
        {
            using LocationContext context = new();

            if (location.UserId is 0)
            {
                var user = AddUser();
                location.UserId = user.Id;
            }

            location.DateAdded = DateTime.Now;

            context.Locations.Add(location);
            context.SaveChanges();

            return location;
        }

        private static User AddUser() {
            using LocationContext context = new();
            var user = new User();

            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }


        public async Task<Location?> GetMostRecentLocationByUserIdAsync(int id)
        {
            using LocationContext context = new();

            return await context.Locations
                .Where(x => x.UserId == id)
                .OrderByDescending(p => p.DateAdded)
                .FirstOrDefaultAsync(); 
        }

        public async Task<IList<Location>> GetAllLocationsByUserIdAsync(int id)
        {
            return await _locationContext.Locations
                .Where(x => x.UserId == id)
                .ToListAsync();
        }

        public async Task<IList<Location>> GetMostRecentLocationForAllUsersAsync()
        {
            using LocationContext context = new();

            return await context.Locations
                .GroupBy(x => x.UserId)
                .Select(x => x
                    .OrderByDescending(x =>x.DateAdded)
                    .First())
                .ToListAsync();
        }
    }
}
