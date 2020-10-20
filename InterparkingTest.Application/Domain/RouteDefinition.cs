using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application.Domain
{
    public class RouteDefinition
    {
        public Coordinates StartPoint { get; set; }
        public Coordinates EndPoint { get; set; }
        /// <summary>
        /// In liters/km
        /// </summary>
        public double CarConsumption { get; set; }
        public double EngineStartEffort { get; set; }
    }
}
