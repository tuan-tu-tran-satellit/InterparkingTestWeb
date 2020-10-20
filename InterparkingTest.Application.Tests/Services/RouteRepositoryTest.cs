using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
