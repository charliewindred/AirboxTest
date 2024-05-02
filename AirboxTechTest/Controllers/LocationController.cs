using Microsoft.AspNetCore.Mvc;
using AirboxTechTest.Models;
using AirboxTechTest.Repositories;

namespace AirboxTechTest.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Get the most recently added location for the given user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        [HttpGet("MostRecentLocation/{userId}")]
        public async Task<ActionResult<Location>> GetMostRecentLocation(int userId)
        {
            var location = await _locationRepository.GetMostRecentLocationByUserIdAsync(userId);

            if (location.Id == 0 || location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        /// <summary>
        /// Gets the most recently added location from all users.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetMostRecentLocationAllUsers()
        {
            var locations = await _locationRepository.GetMostRecentLocationForAllUsersAsync();

            if (locations == null)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        /// <summary>
        /// Gets all locations added by the given user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>
        /// </returns>
        [HttpGet("AllLocations/{userId}")]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocationsByUserIdAsync(int userId)
        {
            var locations = await _locationRepository.GetAllLocationsByUserIdAsync(userId);

            if (locations == null)
            {
                return NotFound();
            }

            return Ok(locations);
        }

        /// <summary>
        /// Adds the given location. 
        /// If userId is not provided, a new user will be created.
        /// </summary>
        /// <returns>
        /// The given location with updated DateAdded property. 
        /// If userId is not provided, the userId property will be updated to the new user's ID.
        /// </returns>
        [HttpPost]
        public ActionResult<Location> AddLocation(LocationDTO locationDTO)
        {
            if (locationDTO == null)
            {
                return BadRequest("Invalid request body");
            }

            var location = new Location
            {
                UserId = locationDTO.UserId,
                X = locationDTO.X,
                Y = locationDTO.Y,
            };

            var createdLocation = _locationRepository.AddLocation(location);
            return CreatedAtAction(nameof(AddLocation), new { id = createdLocation.Id }, LocationToDTO(location));
        }

        private static LocationDTO LocationToDTO(Location location) =>
            new()
            {
                UserId = location.UserId,
                X = location.X,
                Y = location.Y,
                DateAdded = location.DateAdded
            };

    }
}
