using InterparkingTest.Application.Domain;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Route>().OwnsOne<Coordinates>(r => r.StartPoint);
            modelBuilder.Entity<Route>().OwnsOne<Coordinates>(r => r.EndPoint);
        }

        public async Task<List<Route>> GetRoutesAsync(CancellationToken cancellationToken = default)
        {
            return await Routes.ToListAsync(cancellationToken);
        }

        public async Task SaveRouteAsync(Route route, CancellationToken cancellationToken = default)
        {
            bool update = route.Id != 0;
            if(update)
            {
                Routes.Update(route);
            }
            else
            {
                Routes.Add(route);
            }
            await SaveChangesAsync(cancellationToken);
        }

        public void EnsureDatabaseCreated()
        {
            this.Database.EnsureCreated();
        }

        public async Task<Route> GetRouteAsync(int id, CancellationToken cancellation)
        {
            return await Routes.FindAsync(new object[] { id }, cancellation) ?? throw new ArgumentException("Cannot find route with id " + id);
        }
    }
}
