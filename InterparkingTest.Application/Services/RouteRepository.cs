using InterparkingTest.Application.Commands.CreateRoute;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    class RouteRepository : DbContext, IRouteRepository
    {
        public DbSet<Route> Routes { get; set; }

        public RouteRepository(DbContextOptions options)
            : base(options)
        {
        }

        public async Task<List<Route>> GetRoutesAsync(CancellationToken cancellationToken = default)
        {
            return await Routes.ToListAsync(cancellationToken);
        }

        public async Task SaveRouteAsync(Route route, CancellationToken cancellationToken = default)
        {
            Routes.Attach(route);

            await SaveChangesAsync(cancellationToken);
        }
    }
}
