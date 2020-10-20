using FluentAssertions;
using InterparkingTest.Application.Commands.CreateRoute;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    [TestClass]
    public class RouteRepositoryTest
    {

        private SqliteConnection _connection;
        [TestInitialize]
        public void InitConnection()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
        }
        private RouteRepository CreateRepo(bool reset = false)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(_connection);
            var repo = new RouteRepository(optionsBuilder.Options);
            if (reset)
            {
                repo.Database.EnsureDeleted();
                repo.Database.EnsureCreated();
            }
            return repo;
        }

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
            //Arrange
            Route seedRoute = TestData.CreateRoute();

            using (var repo = CreateRepo(reset: true))
            {
                //Act
                await repo.SaveRouteAsync(seedRoute);
            }

            //Assert
            using (var repo = CreateRepo())
            {
                repo.Routes.Count().Should().Be(1);
                var route = repo.Routes.First();
                route.Should().BeEquivalentTo(seedRoute);
                route.Should().NotBeSameAs(seedRoute);
            }
        }

        [TestMethod]
        public async Task UpdateRoute()
        {
            //Arrange
            var seedRoute = CreateSeedRoute();
            seedRoute.Distance = seedRoute.Distance + 100;
            seedRoute.StartPoint = TestData.CreateRoute(5).StartPoint;
            seedRoute.EndPoint = TestData.CreateRoute(7).EndPoint;
            seedRoute.FuelConsumption = seedRoute.FuelConsumption + 124;

            using (var repo = CreateRepo())
            {
                //Act
                await repo.SaveRouteAsync(seedRoute);
            }

            //Assert
            using (var repo = CreateRepo())
            {
                repo.Routes.Count().Should().Be(1);
                var route = repo.Routes.First();
                route.Should().BeEquivalentTo(seedRoute);
                route.Should().NotBeSameAs(seedRoute);
            }
        }

        private Route CreateSeedRoute()
        {
            Route seedRoute = TestData.CreateRoute();
            using (var repo = CreateRepo(reset: true))
            {
                repo.SaveRouteAsync(seedRoute).Wait();
            }
            return seedRoute;
        }
    }
}
