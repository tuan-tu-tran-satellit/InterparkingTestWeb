using FluentAssertions;
using InterparkingTest.Application.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    [TestClass]
    public class RouteModificationServiceTest
    {
        RouteModificationService _routeModificationService;
        Mock<IFuelConsumptionService> _fuel;
        Mock<IRoutingService> _routing;
        Mock<IRouteRepository> _repo;
        CancellationToken _cancellation;

        [TestInitialize]
        public void Init()
        {
            _cancellation = new CancellationToken();

            _fuel = new Mock<IFuelConsumptionService>(MockBehavior.Strict);
            _routing = new Mock<IRoutingService>(MockBehavior.Strict);
            _repo = new Mock<IRouteRepository>(MockBehavior.Strict);

            _routeModificationService = new RouteModificationService(_routing.Object, _fuel.Object, _repo.Object);
        }

        [TestMethod]
        public async Task AddRoute()
        {
            //Arrange
            var routeDefinition = TestData.CreateRouteDefinition();
            double distance = 6.7;
            _routing.Setup(s => s.CalculateDistanceAsync(routeDefinition.StartPoint, routeDefinition.EndPoint, _cancellation)).ReturnsAsync(distance);

            double fuel = 12.3;
            Route fuelRoute = null;
            _fuel.Setup(s => s.CalculateFuelConsumptionAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => fuelRoute = route)
                .ReturnsAsync(fuel)
            ;

            Route savedRoute = null;
            _repo.Setup(s => s.SaveRouteAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => savedRoute = route)
                .Returns(Task.CompletedTask)
            ;

            //act
            await _routeModificationService.AddRouteAsync(routeDefinition, _cancellation);

            //Assert
            Mock.VerifyAll(_routing, _repo, _fuel);

            savedRoute.Should().BeEquivalentTo(new Route()
            {
                StartPoint = routeDefinition.StartPoint,
                EndPoint = routeDefinition.EndPoint,
                EngineStartEffort = routeDefinition.EngineStartEffort,
                CarConsumption = routeDefinition.CarConsumption,
                Distance = distance,
                FuelConsumption = fuel,
            });
            fuelRoute.Should().BeEquivalentTo(routeDefinition);
        }

        [TestMethod]
        public async Task UpdateRoute()
        {
            //Arrange
            var routeId = 1234;
            var routeDefinition = TestData.CreateRouteDefinition();
            double distance = 6.7;
            _routing.Setup(s => s.CalculateDistanceAsync(routeDefinition.StartPoint, routeDefinition.EndPoint, _cancellation)).ReturnsAsync(distance);

            double fuel = 12.3;
            Route fuelRoute = null;
            _fuel.Setup(s => s.CalculateFuelConsumptionAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => fuelRoute = route)
                .ReturnsAsync(fuel)
            ;

            Route savedRoute = null;
            _repo.Setup(s => s.SaveRouteAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => savedRoute = route)
                .Returns(Task.CompletedTask)
            ;

            //act
            await _routeModificationService.UpdateRouteAsync(routeId, routeDefinition, _cancellation);

            //Assert
            Mock.VerifyAll(_routing, _repo, _fuel);

            savedRoute.Should().BeEquivalentTo(new Route()
            {
                Id = routeId,
                StartPoint = routeDefinition.StartPoint,
                EndPoint = routeDefinition.EndPoint,
                EngineStartEffort = routeDefinition.EngineStartEffort,
                CarConsumption = routeDefinition.CarConsumption,
                Distance = distance,
                FuelConsumption = fuel,
            });
            fuelRoute.Should().BeEquivalentTo(routeDefinition);
        }

    }
}
