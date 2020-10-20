using System;
using System.Collections.Generic;
using System.Linq;
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

        public IActionResult Index()
        {
            return View(new List<Route>());
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
        public IActionResult Create(RouteDefinition route)
        {
            if (ModelState.IsValid)
            {
                _logger.LogDebug("save route: {0}");
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
