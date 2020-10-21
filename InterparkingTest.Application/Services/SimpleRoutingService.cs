using InterparkingTest.Application.Domain;
using InterparkingTest.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Infrastructure
{
    class SimpleRoutingService : IRoutingService
    {
        public Task<double?> CalculateDistanceAsync(Coordinates startPoint, Coordinates endPoint, CancellationToken _)
        {
            //Simple implementation that calculates the "crow-flight" distance
            var a = new Geolocation.Coordinate()
            {
                Latitude = startPoint.Latitude,
                Longitude = endPoint.Longitude,
            };

            var b = new Geolocation.Coordinate()
            {
                Latitude = endPoint.Latitude,
                Longitude = endPoint.Longitude,
            };

            double? distance = Geolocation.GeoCalculator.GetDistance(a, b, decimalPlaces: 3, distanceUnit: Geolocation.DistanceUnit.Kilometers);

            return Task.FromResult(distance);
        }
    }
}
