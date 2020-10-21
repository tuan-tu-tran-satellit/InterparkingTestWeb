using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InterparkingTest.Application;
using InterparkingTest.Application.Domain;
using InterparkingTestWebApp.Models;
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
            SetTitle(_TITLE_CREATE);
            return View(_FORM_VIEW_NAME);
        }
        public async Task<IActionResult> Edit(int id, CancellationToken cancellation)
        {
            SetTitle(_TITLE_EDIT);
            return View(_FORM_VIEW_NAME, new RouteFormData()
            {
                Id = id,
                Route = await _application.GetRouteDefinition(id, cancellation),
            });
        }

        private void SetTitle(string title)
        {
            ViewData["Title"] = title;
        }

        const string _FORM_VIEW_NAME = "Form";
        const string _TITLE_CREATE = "Create new route";
        const string _TITLE_EDIT = "Edit route";

        [HttpPost]
        public async Task<IActionResult> Save(RouteFormData formData, CancellationToken cancellationToken)
        {
            //LogModelStateInfo(route);
            if (ModelState.IsValid)
            {
                RouteModificationResult result;
                if (formData.Id == null)
                {
                    result = await _application.AddRouteAsync(formData.Route, cancellationToken);
                }
                else
                {
                    result = await _application.UpdateRoute(formData.Id.Value, formData.Route, cancellationToken);
                }
                if (result.IsSuccess)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ViewData["ErrorMessage"] = "Could not calculate the distance between those locations. Please adjust the coordinates";
                }
            }
            //This code normally never gets executed since there's client side validation and no specific server-side validation
            SetTitle(formData.Id == null ? _TITLE_CREATE : _TITLE_EDIT);
            return View(_FORM_VIEW_NAME, formData);
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
