using InterparkingTest.Application.Domain;
using System;

namespace InterparkingTest.Application
{
    internal static class TestData
    {
        internal static RouteDefinition CreateRouteDefinition(int offset = 0)
        {
            return new RouteDefinition()
            {
                CarConsumption = 12.2 + offset,
                EngineStartEffort = 2.3 + offset,
                StartPoint = new Coordinates()
                {
                    Latitude = 54.312343 + offset * 1e-6,
                    Longitude = 4.231456 + offset * 1e-6,
                },
                EndPoint = new Coordinates()
                {
                    Latitude = 54.334522 + offset * 1e-6,
                    Longitude = 4.029384 + offset * 1e-6,
                },
            };
        }

        internal static Route CreateRoute(int offset = 0)
        {
            return new Route()
            {
                CarConsumption = 12.2 + offset,
                EngineStartEffort = 2.3 + offset,
                StartPoint = new Coordinates()
                {
                    Latitude = 54.312343 + offset * 1e-6,
                    Longitude = 4.231456 + offset * 1e-6,
                },
                EndPoint = new Coordinates()
                {
                    Latitude = 54.334522 + offset * 1e-6,
                    Longitude = 4.029384 + offset * 1e-6,
                },
                Distance = 123,
                FuelConsumption = 456,
            };
        }
    }
}