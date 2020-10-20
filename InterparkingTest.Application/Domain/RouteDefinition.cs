using InterparkingTest.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Commands.CreateRoute
{
    public class RouteDefinition
    {
        public Coordinates StartPoint { get; set; }
        public Coordinates EndPoint { get; set; }
        public double CarConsumption { get; set; }
        public double EngineStartEffort { get; set; }
    }
}
