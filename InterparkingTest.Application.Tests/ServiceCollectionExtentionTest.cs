using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace InterparkingTest.Application
{
    [TestClass]
    public class ServiceCollectionExtentionTest
    {
        [TestMethod]
        public void ResolveApplicationFacade()
        {
            //Arrange
            var services = new ServiceCollection();
            services.AddInterparkingTestApplicationServices();
            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                //Act
                Action resolve = () =>
                    scope.ServiceProvider.GetRequiredService<IApplicationFacade>();

                //Assert
                resolve.Should().NotThrow();
            }
        }
    }
}
