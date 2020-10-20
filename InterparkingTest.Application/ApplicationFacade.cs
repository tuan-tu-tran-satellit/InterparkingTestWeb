using InterparkingTest.Application.Commands.CreateRoute;
using System;
using System.Collections.Generic;
using System.Text;
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
        public async Task AddRouteAsync(RouteDefinition command)
        {
            await _routeModificationService.AddRouteAsync(command);
        }

        public async Task UpdateRoute(int id, RouteDefinition route)
        {
            await _routeModificationService.UpdateRouteAsync(id, route);
        }

        public async Task<List<Route>> GetRoutes()
        {
            return await _routeRepository.GetRoutesAsync();
        }
    }
}
