using AirboxTechTest.Context;
using AirboxTechTest.Controllers;
using AirboxTechTest.Models;
using AirboxTechTest.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AirboxTechTest.Tests
{
    public class LocationControllerTests
    {
        [Fact]
        public async void GetMostRecentLocation_WithValidUserId_ReturnsOkResult()
        {
            var location = new Location
            {
                DateAdded = DateTime.Now,
                Id = 1,
                UserId = 1,
                X = 1,
                Y = 1
            };
            var respository = new Mock<ILocationRepository>();
            respository.Setup(x => x.GetMostRecentLocationByUserIdAsync(1)).ReturnsAsync(location);
                
            var controller = new LocationController(respository.Object);

            var result = await controller.GetMostRecentLocation(1);

            Assert.IsType<ActionResult<Location>>(result);
        }        
    }
}