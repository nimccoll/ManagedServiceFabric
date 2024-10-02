//===============================================================================
// Microsoft FastTrack for Azure
// Application Insights Demos
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using AIASPNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIASPNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("This is an informational message from the ILogger interface.");
            Trace.TraceInformation("This is an informational message from the TraceListener interface.");

            _logger.LogError(new ApplicationException("This is an application exception logged by the ILogger interface."), string.Empty, null);
            Trace.TraceError("This is an application exception logged by the TraceListener interface.");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
