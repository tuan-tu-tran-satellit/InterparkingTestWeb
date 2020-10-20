using InterparkingTest.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    class CreateRouteCommandHandler
    {
        private readonly IRoutingService _routingService;
        private readonly IFuelConsumptionService _fuelConsumptionService;
        private readonly IRouteRepository _routeRepository;

        public CreateRouteCommandHandler(IRoutingService routingService, IFuelConsumptionService fuelConsumptionService, IRouteRepository routeRepository)
        {
            _routingService = routingService;
            _fuelConsumptionService = fuelConsumptionService;
            _routeRepository = routeRepository;
        }

        public void Handle(RouteDefinition command)
        {
            Route route = new Route()
            {
                StartPoint = command.StartPoint,
                EndPoint = command.EndPoint,
                CarConsumption = command.CarConsumption,
                EngineStartEffort = command.EngineStartEffort,
            };

            route.Distance = _routingService.CalculateDistance(route.StartPoint, route.EndPoint);

            route.FuelConsumption = _fuelConsumptionService.CalculateFuelConsumption(route);

            _routeRepository.SaveRoute(route);
        }
    }
}
