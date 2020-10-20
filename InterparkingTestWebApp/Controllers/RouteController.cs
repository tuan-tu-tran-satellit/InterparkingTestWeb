using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterparkingTestWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace InterparkingTestWebApp.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<Route>());
        }
    }
}
