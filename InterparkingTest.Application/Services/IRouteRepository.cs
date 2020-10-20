﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    public interface IRouteRepository
    {
        Task<List<Route>> GetRoutesAsync(CancellationToken cancellationToken = default);
        Task SaveRouteAsync(Route route, CancellationToken cancellationToken = default);
    }
}