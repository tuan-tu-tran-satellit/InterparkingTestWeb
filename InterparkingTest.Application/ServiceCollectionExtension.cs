using InterparkingTest.Application.Config;
using InterparkingTest.Application.Infrastructure;
using InterparkingTest.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterparkingTest.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInterparkingTestApplicationServices(this IServiceCollection services, IConfiguration configuration = null, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            services
                .AddScoped<IApplicationFacade, ApplicationFacade>()
                .AddDbContext<IRouteRepository, RouteRepository>(optionsAction)
                .AddScoped<IRouteModificationService, RouteModificationService>()
                .AddScoped<IRoutingService, AzureMapsRoutingService>()
                .AddScoped<IFuelConsumptionService, FuelConsumptionService>()
                .AddScoped<IHttpClient, HttpClient>()
                .AddHttpClient()
            ;

            services.Configure<AzureMapsOptions>(options =>
            {
                configuration?.GetSection(nameof(AzureMapsOptions)).Bind(options);
            });
            return services;
        }
    }
}
