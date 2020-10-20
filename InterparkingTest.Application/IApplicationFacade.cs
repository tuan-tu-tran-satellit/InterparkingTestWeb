using InterparkingTest.Application.Commands.CreateRoute;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    public interface IApplicationFacade
    {
        Task AddRouteAsync(RouteDefinition command);
        Task<List<Route>> GetRoutes();
        Task UpdateRoute(int id, RouteDefinition route);
    }
}