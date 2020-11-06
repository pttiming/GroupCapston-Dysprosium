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

        public async Task<IActionResult> Index()
        {
            string searchlocation = "53005";
            string searchtype = "pizza";
            var yelpResult = await _yelp.GetBusinesses(searchlocation, searchtype);

<<<<<<< HEAD
=======
            string yelpId = "R-r0sJ-7ntM9ooj7vTK2eg";
            var singleResult = await _yelp.GetBusiness(yelpId);

>>>>>>> 8e102d87b8494a8cfffe9712f1647cd46a2f1df7
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
