using FluentAssertions;
using Geolocation;
using InterparkingTest.Application.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Infrastructure
{
    [TestClass]
    public class SimpleRoutingServiceTest
    {
        [TestMethod]
        public async Task CalculateDistance()
        {
            var routingService = new SimpleRoutingService();
            var a = new Coordinates()
            {
                Latitude = 50.125468,
                Longitude = 4.136589,
            };

            var b = new Coordinates()
            {
                Latitude = 54.126857,
                Longitude = 4.879536,
            };

            var distance = await routingService.CalculateDistanceAsync(a, b, new System.Threading.CancellationToken());
            distance.Should().BeApproximately(444, 1);
        }

    }
}
