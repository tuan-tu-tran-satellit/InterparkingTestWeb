using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterparkingTestWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InterparkingTestWebApp.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;

        public RouteController(ILogger<RouteController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new List<Route>());
        }

        public IActionResult Create()
        {
            var defaultRoute = new Route()
            {
                StartLatitude = 123,
                StartLongitude = 123,
                StopLatitude = 123,
                StopLongitude = 123,
            };
            return View(defaultRoute);
        }

        [HttpPost]
        public IActionResult Create(Route route)
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
