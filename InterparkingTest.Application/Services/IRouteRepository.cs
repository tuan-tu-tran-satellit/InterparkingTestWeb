using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutesAsync();
        Task SaveRouteAsync(Route route);
    }
}