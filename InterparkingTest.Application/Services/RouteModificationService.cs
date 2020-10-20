using InterparkingTest.Application.Commands.CreateRoute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    class RouteModificationService : IRouteModificationService
    {
        private readonly IRoutingService _routingService;
        private readonly IFuelConsumptionService _fuelConsumptionService;
        private readonly IRouteRepository _routeRepository;

        public RouteModificationService(IRoutingService routingService, IFuelConsumptionService fuelConsumptionService, IRouteRepository routeRepository)
        {
            _routingService = routingService;
            _fuelConsumptionService = fuelConsumptionService;
            _routeRepository = routeRepository;
        }

        public async Task AddRouteAsync(RouteDefinition definition)
        {
            await HandleRouteModification(definition, null);
        }

        public async Task UpdateRouteAsync(int id, RouteDefinition route)
        {
            await HandleRouteModification(route, id);
        }

        private async Task HandleRouteModification(RouteDefinition definition, int? id)
        {
            Route route = new Route()
            {
                StartPoint = definition.StartPoint,
                EndPoint = definition.EndPoint,
                CarConsumption = definition.CarConsumption,
                EngineStartEffort = definition.EngineStartEffort,
            };

            if (id.HasValue)
            {
                route.Id = id.Value;
            }

            route.Distance = await _routingService.CalculateDistanceAsync(route.StartPoint, route.EndPoint);

            route.FuelConsumption = await _fuelConsumptionService.CalculateFuelConsumptionAsync(route);

            await _routeRepository.SaveRouteAsync(route);
        }
    }
}
