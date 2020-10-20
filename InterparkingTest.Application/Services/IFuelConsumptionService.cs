using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    public interface IFuelConsumptionService
    {
        Task<double> CalculateFuelConsumptionAsync(Route route, CancellationToken cancellation);
    }
}