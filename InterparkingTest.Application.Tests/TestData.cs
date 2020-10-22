using InterparkingTest.Application.Domain;
using Microsoft.Extensions.Logging;
using System;

namespace InterparkingTest.Application
{
    internal static class TestData
    {
        internal static RouteDefinition CreateRouteDefinition(int offset = 0)
        {
            return new RouteDefinition()
            {
                StartDescription = "start description " + offset,
                EndDescription = "end description " + offset,
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
                StartDescription = "start description " + offset,
                EndDescription = "end description " + offset,
            };
        }

        internal static RouteModificationResult CreateRoutModificationResult()
        {
            return new RouteModificationResult()
            {
                IsSuccess = true
            };
        }

        internal static ILogger<T> GetLogger<T>()
        {
            return
                LoggerFactory.Create(
                    logging =>
                    {
                        logging.SetMinimumLevel(LogLevel.Trace);
                        logging.AddConsole();
                    }
                )
                .CreateLogger<T>()
            ;
        }
    }
}