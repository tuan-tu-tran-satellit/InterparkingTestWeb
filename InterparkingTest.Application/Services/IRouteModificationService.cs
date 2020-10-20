using InterparkingTest.Application.Commands.CreateRoute;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    public interface IRouteModificationService
    {
        Task UpdateRouteAsync(int id, RouteDefinition route);
        Task AddRouteAsync(RouteDefinition command);
    }
}