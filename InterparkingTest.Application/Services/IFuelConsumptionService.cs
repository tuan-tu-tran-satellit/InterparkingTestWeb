using InterparkingTest.Application.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    public interface IFuelConsumptionService
    {
        Task<double> CalculateFuelConsumptionAsync(Route route, CancellationToken cancellation);
    }
}