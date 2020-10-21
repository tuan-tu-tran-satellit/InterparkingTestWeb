using FluentAssertions;
using InterparkingTest.Application.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    [TestClass]
    public class FuelConsumptionServiceTest
    {
        [TestMethod]
        public async Task CalculateFuelConsumption()
        {
            //arrange
            var fuelService = new FuelConsumptionService();
            Route route = new Route()
            {
                Distance = 100, //km
                CarConsumption = 6, //L/100 km
            };
            var fuel = await fuelService.CalculateFuelConsumptionAsync(route, new CancellationToken());
            fuel.Should().Be(6);
        }
    }
}
