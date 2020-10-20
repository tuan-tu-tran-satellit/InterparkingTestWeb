using InterparkingTest.Application.Commands.CreateRoute;
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
            var result = route.Distance * route.CarConsumption;
            //Consider using route.EngineStartEffort
            
            return Task.FromResult(result);
        }
    }
}
