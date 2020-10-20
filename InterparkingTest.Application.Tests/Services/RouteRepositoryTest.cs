using FluentAssertions;
using InterparkingTest.Application.Commands.CreateRoute;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    [TestClass]
    public class RouteRepositoryTest
    {
        [TestMethod]
        public void ModelIsValidWithInMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("InterparkingTest");

            var repo = new RouteRepository(optionsBuilder.Options);

            //Just accessing the model is enough to validate it
            var model = repo.Model;
        }

        [TestMethod]
        public void ModelIsValidWithSQLite()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite("Filename=:memory:");

            var repo = new RouteRepository(optionsBuilder.Options);

            //Just accessing the model is enough to validate it
            var model = repo.Model;
        }

        [TestMethod]
        public async Task AddRoute()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            optionsBuilder.UseSqlite(connection);

            Route seedRoute = TestData.CreateRoute();
            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                repo.Database.EnsureCreated();
                await repo.SaveRouteAsync(seedRoute);
            }

            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                repo.Routes.Count().Should().Be(1);
                var route = repo.Routes.First();
                route.Should().BeEquivalentTo(seedRoute);
                route.Should().NotBeSameAs(seedRoute);
            }

            seedRoute.Distance = seedRoute.Distance + 100;
            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                await repo.SaveRouteAsync(seedRoute);
            }

            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                repo.Routes.Count().Should().Be(1);
                var route = repo.Routes.First();
                route.Should().BeEquivalentTo(seedRoute);
                route.Should().NotBeSameAs(seedRoute);
            }

            seedRoute.StartPoint = TestData.CreateRoute(5).StartPoint;
            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                await repo.SaveRouteAsync(seedRoute);
            }

            using (var repo = new RouteRepository(optionsBuilder.Options))
            {
                repo.Routes.Count().Should().Be(1);
                var route = repo.Routes.First();
                route.Should().BeEquivalentTo(seedRoute);
                route.Should().NotBeSameAs(seedRoute);
            }
        }
    }
}
