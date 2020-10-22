using InterparkingTest.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

        public async Task<RouteModificationResult> AddRouteAsync(RouteDefinition definition, CancellationToken cancellation)
        {
            return await HandleRouteModification(definition, null, cancellation);
        }

        public async Task<RouteModificationResult> UpdateRouteAsync(int id, RouteDefinition route, CancellationToken cancellation)
        {
            return await HandleRouteModification(route, id, cancellation);
        }

        private async Task<RouteModificationResult> HandleRouteModification(RouteDefinition definition, int? id, CancellationToken cancellation)
        {
            Route route = new Route()
            {
                StartDescription = definition.StartDescription,
                EndDescription = definition.EndDescription,
                StartPoint = definition.StartPoint,
                EndPoint = definition.EndPoint,
                CarConsumption = definition.CarConsumption ?? 0,
                EngineStartEffort = definition.EngineStartEffort ?? 0,
            };

            if (id.HasValue)
            {
                route.Id = id.Value;
            }

            var distance = await _routingService.CalculateDistanceAsync(route.StartPoint, route.EndPoint, cancellation);
            if (distance == null)
                return new RouteModificationResult { IsSuccess = false };
            route.Distance = distance.Value;

            route.FuelConsumption = await _fuelConsumptionService.CalculateFuelConsumptionAsync(route, cancellation);

            await _routeRepository.SaveRouteAsync(route, cancellation);
            return new RouteModificationResult() { IsSuccess = true };
        }
    }
}
