using InterparkingTest.Application.Commands.CreateRoute;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    public interface IRouteModificationService
    {
        Task UpdateRouteAsync(int id, RouteDefinition route, CancellationToken cancellation);
        Task AddRouteAsync(RouteDefinition command, CancellationToken cancellation);
    }
}