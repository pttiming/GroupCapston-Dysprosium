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
using System.Security.Claims;
using GroupCapstone.Data;
using Microsoft.AspNetCore.Identity;

namespace GroupCapstone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public YelpService _yelp;

        public ApplicationDbContext _db;

        public GroupChatContext _context;
        public UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, YelpService yelp, ApplicationDbContext db, GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _yelp = yelp;
            _db = db;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            //if (participant == null)
            //{
            //    return RedirectToAction("Create", "Participants");
            //}
            return RedirectToAction("Index", "Chat");
        }

        public Task<YelpBusiness> GetBusiness(string businessId)
        {
            var yelpResult = _yelp.GetBusiness(businessId);
            return yelpResult;
        }

        public Task<YelpBusinesses> GetBusinesses(string location, string type)
        {
            var yelpResult = _yelp.GetBusinesses(location, type);
            return yelpResult;
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

        public IActionResult IndexList()
        {
            GroupUsersViewModel groupUsersViewModel = new GroupUsersViewModel();
            groupUsersViewModel.Groups = GetAllGroups();
            groupUsersViewModel.UserGroups = GetAllUserGroups();
            groupUsersViewModel.UserName = GetUserName();
            
            return View(groupUsersViewModel);

        }

        public List<Group> GetAllGroups()
        {
            var groups = _context.Groups.ToList();
            return groups;
        }

        public List<UserGroup> GetAllUserGroups()
        {
            var userGroups = _context.UserGroup.ToList();
            return userGroups;
        }

        public string GetUserName()
        {
            string userName;
            userName = this.User.Identity.Name;
            return userName;
        }

        public ActionResult JoinGroup(int groupid)
        {
            UserGroup userGroup = new UserGroup();
            userGroup.GroupId = groupid;
            userGroup.UserName = GetUserName();
            _context.UserGroup.Add(userGroup);
            _context.SaveChanges();

            return RedirectToAction(nameof(IndexList));
        }

    }
}
