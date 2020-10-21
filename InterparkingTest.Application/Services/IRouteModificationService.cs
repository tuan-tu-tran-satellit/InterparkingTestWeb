using InterparkingTest.Application.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    public interface IRouteModificationService
    {
        Task<RouteModificationResult> UpdateRouteAsync(int id, RouteDefinition route, CancellationToken cancellation);
        Task<RouteModificationResult> AddRouteAsync(RouteDefinition command, CancellationToken cancellation);
    }
}