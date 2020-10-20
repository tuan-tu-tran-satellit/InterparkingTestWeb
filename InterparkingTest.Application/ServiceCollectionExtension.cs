using InterparkingTest.Application.Commands.CreateRoute;
using InterparkingTest.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInterparkingTestApplicationServices(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services
                .AddScoped<IApplicationFacade, ApplicationFacade>()
                .AddDbContext<IRouteRepository, RouteRepository>(optionsAction)
                .AddScoped<IRouteModificationService, RouteModificationService>()
                .AddScoped<IRoutingService, RoutingService>()
                .AddScoped<IFuelConsumptionService, FuelConsumptionService>()
            ;
            return services;
        }
    }
}
