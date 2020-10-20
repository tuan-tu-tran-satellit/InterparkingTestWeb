using InterparkingTest.Application.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    public interface IRoutingService
    {
        Task<double> CalculateDistanceAsync(Coordinates startPoint, Coordinates endPoint, CancellationToken cancellation);
    }
}