﻿using InterparkingTest.Application.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application
{
    public interface IApplicationFacade
    {
        Task AddRouteAsync(RouteDefinition command, CancellationToken cancellation);
        Task<List<Route>> GetRoutes(CancellationToken cancellation);
        Task UpdateRoute(int id, RouteDefinition route, CancellationToken cancellation);
        void EnsureDatabaseCreated();
        Task<RouteDefinition> GetRouteDefinition(int id, CancellationToken cancellation);
    }
}