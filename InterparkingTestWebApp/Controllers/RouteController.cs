using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterparkingTest.Application;
using InterparkingTest.Application.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace InterparkingTestWebApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;
        private readonly IApplicationFacade _application;

        public RouteController(ILogger<RouteController> logger, IApplicationFacade application)
        {
            _logger = logger;
            _application = application;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            return View(await _application.GetRoutes(cancellationToken));
        }

        public IActionResult Create()
        {
            var defaultRoute = new RouteDefinition
            {
                StartPoint = new Coordinates()
                {
                    Latitude = 54.123453,
                    Longitude = 4.342134,
                },
                EndPoint = new Coordinates()
                {
                    Latitude = 54.365487,
                    Longitude = 4.365987,
                },
                CarConsumption = 6.2,
                EngineStartEffort = 0,
            };
            return View(defaultRoute);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RouteDefinition route, CancellationToken cancellationToken)
        {
            //LogModelStateInfo(route);
            if (ModelState.IsValid)
            {
                _logger.LogDebug("save route: {0}");
                await _application.AddRouteAsync(route, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        //We could delete this but I'm just keeping it around, just in case I want to log this info again some day.
        //I wrote this while troubleshooting the coma vs period issue and I figured it might help some day.
        private void LogModelStateInfo(RouteDefinition route)
        {
            using (var output = new StringWriter())
            {
                output.WriteLine("Create: ModelState {0}", ModelState.IsValid);
                foreach (var item in ModelState)
                {
                    output.WriteLine($"{item.Key} : {item.Value.AttemptedValue} : {item.Value.ValidationState} : {String.Join(" | ", item.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                output.WriteLine(JsonConvert.SerializeObject(route, Formatting.Indented));
                output.WriteLine($"culture info : {CultureInfo.CurrentCulture} : {CultureInfo.CurrentUICulture}");
                _logger.LogDebug(output.ToString());
            }
        }
    }
}
