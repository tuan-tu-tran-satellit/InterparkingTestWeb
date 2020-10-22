using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    [TestClass]
    public class RouteDefinitionTest
    {
        [TestMethod]
        public void FromRoute()
        {
            //Arrange
            var route = TestData.CreateRoute();

            //Act
            var definition = RouteDefinition.FromRoute(route);

            //Assert
            definition.Should().BeEquivalentTo(route);
        }
    }
}
