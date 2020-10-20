using InterparkingTest.Application.Domain;
using InterparkingTest.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    internal class ApplicationFacade : IApplicationFacade
    {
        private readonly IRouteModificationService _routeModificationService;
        private readonly IRouteRepository _routeRepository;

        public ApplicationFacade(IRouteModificationService routeModificationService, IRouteRepository routeRepository)
        {
            _routeModificationService = routeModificationService;
            _routeRepository = routeRepository;
        }
        public async Task AddRouteAsync(RouteDefinition command, CancellationToken cancellation)
        {
            await _routeModificationService.AddRouteAsync(command, cancellation);
        }

        public async Task UpdateRoute(int id, RouteDefinition route, CancellationToken cancellation)
        {
            await _routeModificationService.UpdateRouteAsync(id, route, cancellation);
        }

        public async Task<List<Route>> GetRoutes(CancellationToken cancellationToken)
        {
            return await _routeRepository.GetRoutesAsync(cancellationToken);
        }
    }
}
