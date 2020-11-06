using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroupCapstone.Models;
using GroupCapstone.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace GroupCapstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public YelpService _yelp;

        public HomeController(ILogger<HomeController> logger, YelpService yelp)
        {
            _logger = logger;
            _yelp = yelp;
        }

        public IActionResult Index()
        {

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
