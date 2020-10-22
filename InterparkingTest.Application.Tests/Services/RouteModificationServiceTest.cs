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
        [DynamicData(nameof(GetTestRouteDefinitions), DynamicDataSourceType.Method)]
        public async Task AddRoute(string caseName, RouteDefinition routeDefinition)
        {
            //Arrange
            double distance = 6.7;
            _routing.Setup(s => s.CalculateDistanceAsync(routeDefinition.StartPoint, routeDefinition.EndPoint, _cancellation)).ReturnsAsync(distance);

            double fuel = 12.3;
            Route routeUsedToCalculateFuel = null;
            _fuel.Setup(s => s.CalculateFuelConsumptionAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => routeUsedToCalculateFuel = route)
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

            routeDefinition.CarConsumption ??= 0;
            routeDefinition.EngineStartEffort ??= 0;

            savedRoute.Should().BeEquivalentTo(routeDefinition);
            savedRoute.Distance.Should().Be(distance);
            savedRoute.FuelConsumption.Should().Be(fuel);
            routeUsedToCalculateFuel.Should().BeSameAs(savedRoute);
        }

        [TestMethod]
        [DynamicData(nameof(GetTestRouteDefinitions), DynamicDataSourceType.Method)]
        public async Task UpdateRoute(string caseName, RouteDefinition routeDefinition)
        {
            //Arrange
            var routeId = 1234;
            double distance = 6.7;
            _routing.Setup(s => s.CalculateDistanceAsync(routeDefinition.StartPoint, routeDefinition.EndPoint, _cancellation)).ReturnsAsync(distance);

            double fuel = 12.3;
            Route routeUsedForFuelCalculation = null;
            _fuel.Setup(s => s.CalculateFuelConsumptionAsync(It.IsAny<Route>(), _cancellation))
                .Callback<Route, CancellationToken>((route, _) => routeUsedForFuelCalculation = route)
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

            routeDefinition.CarConsumption ??= 0;
            routeDefinition.EngineStartEffort ??= 0;

            savedRoute.Should().BeEquivalentTo(routeDefinition);
            savedRoute.Distance.Should().Be(distance);
            savedRoute.FuelConsumption.Should().Be(fuel);
            routeUsedForFuelCalculation.Should().BeSameAs(savedRoute);
        }

        public static IEnumerable<object[]> GetTestRouteDefinitions()
        {
            RouteDefinition routeDefinition = TestData.CreateRouteDefinition();
            yield return new object[] { "normal case", routeDefinition };

            routeDefinition.CarConsumption = null;
            yield return new object[] { "car consumption is null", routeDefinition };

            routeDefinition.CarConsumption = 6.7;
            routeDefinition.EngineStartEffort = null;
            yield return new object[] { "engine start effort is null", routeDefinition };
        }

    }
}
