using InterparkingTest.Application.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutesAsync(CancellationToken cancellationToken);
        Task SaveRouteAsync(Route route, CancellationToken cancellationToken);
        void EnsureDatabaseCreated();
    }
}