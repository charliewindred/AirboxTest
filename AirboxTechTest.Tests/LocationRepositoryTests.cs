using AirboxTechTest.Context;
using AirboxTechTest.Models;
using AirboxTechTest.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace AirboxTechTest.Tests
{
    public class LocationRepositoryTests
    {
        [Fact]
        public async void GetAllLocationsByUserIdAsync_WithValidId_ReturnsCorrectLocations()
        {
            var data = new List<Location>
            {
                new() { Id = 1, UserId = 1 },
                new() { Id = 1, UserId = 1 },
                new() { Id = 1, UserId = 2 },
            };

            var contextMock = new Mock<LocationContext>();
            contextMock.Setup<DbSet<Location>>(x => x.Locations)
                .ReturnsDbSet(data);

            var locationRepo = new LocationRepository(contextMock.Object);
            var result = await locationRepo.GetAllLocationsByUserIdAsync(1);

            Assert.True(result.Count == 2);
        }
    }
}
