using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterparkingTest.Application;
using InterparkingTest.Application.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
                    Latitude = 123,
                    Longitude = 123,
                },
                EndPoint = new Coordinates()
                {
                    Latitude = 1234,
                    Longitude = 345,
                },
                CarConsumption = 345,
                EngineStartEffort = 0,
            };
            return View(defaultRoute);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RouteDefinition route, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("save route: {0}");
                await _application.AddRouteAsync(route, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
