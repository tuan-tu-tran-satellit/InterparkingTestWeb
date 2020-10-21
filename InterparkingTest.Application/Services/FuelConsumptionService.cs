using InterparkingTest.Application.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterparkingTest.Application.Services
{
    class FuelConsumptionService : IFuelConsumptionService
    {
        public Task<double> CalculateFuelConsumptionAsync(Route route, CancellationToken _)
        {
            var result = route.Distance * route.CarConsumption / 100;
            //Consider using route.EngineStartEffort
            
            return Task.FromResult(result);
        }
    }
}
