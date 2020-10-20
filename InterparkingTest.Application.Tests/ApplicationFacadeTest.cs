using InterparkingTest.Application.Commands.CreateRoute;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    [TestClass]
    public class ApplicationFacadeTest
    {
        ApplicationFacade _applicationFacade;
        Mock<IRouteModificationService> _routeModificationService;
        Mock<IRouteRepository> _routeRepo;
        CancellationToken _cancellationToken;

        [TestInitialize]
        public void Initialize()
        {
            _routeModificationService = new Mock<IRouteModificationService>(MockBehavior.Strict);
            _routeRepo = new Mock<IRouteRepository>(MockBehavior.Strict);
            _cancellationToken = new CancellationToken();
            _applicationFacade = new ApplicationFacade(_routeModificationService.Object, _routeRepo.Object);
        }

        [TestMethod]
        public async Task AddRoute()
        {
            //Arrange
            RouteDefinition route = TestData.CreateRouteDefinition();
            _routeModificationService.Setup(s => s.AddRouteAsync(route, _cancellationToken)).Returns(Task.CompletedTask);

            //Act
            await _applicationFacade.AddRouteAsync(route, _cancellationToken);
            
            //Assert
            _routeModificationService.VerifyAll();
        }

        [TestMethod]
        public async Task UpdateRoute()
        {
            //Arrange
            RouteDefinition route = TestData.CreateRouteDefinition();
            int routeId = 4567;
            _routeModificationService.Setup(s => s.UpdateRouteAsync(routeId, route, _cancellationToken)).Returns(Task.CompletedTask);
            
            //Act
            await _applicationFacade.UpdateRoute(routeId, route, _cancellationToken);

            //Assert
            _routeModificationService.VerifyAll();
        }

        [TestMethod]
        public async Task GetRoutes()
        {
            //Arrange
            var cancellationToken = new CancellationToken();
            _routeRepo.Setup(s => s.GetRoutesAsync(cancellationToken)).ReturnsAsync(new List<Route>());

            //Act
            await _applicationFacade.GetRoutes(cancellationToken);

            //Assert
            _routeRepo.VerifyAll();
        }
    }
}
