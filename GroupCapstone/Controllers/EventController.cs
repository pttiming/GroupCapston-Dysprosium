using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupCapstone.Data;
using GroupCapstone.Models;
using GroupCapstone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupCapstone.Controllers
{
    public class EventController : Controller
    {
        public YelpService _yelp;

        public ApplicationDbContext _db;

        public GoogleService _google;

        public UserManager<IdentityUser> _userManager;

        public EventController(YelpService yelp, ApplicationDbContext db, GoogleService google, UserManager<IdentityUser> userManager)
        {
            _yelp = yelp;
            _db = db;
            _google = google;
            _userManager = userManager;
        }


        public IActionResult Create(Event groupChatEvent)
        {

            return RedirectToAction("Chat", "Index");
        }

        public  IActionResult Index()
        {
            EventUserGroupViewModel eventUserGroupViewModel = new EventUserGroupViewModel();
            eventUserGroupViewModel.UserGroups = GetAllUserGroups();
            eventUserGroupViewModel.Groups = GetAllGroups();
            eventUserGroupViewModel.UserName = GetUserName();
            eventUserGroupViewModel.Events = GetAllEvents();

            return View(eventUserGroupViewModel);
        }

        public List<Group> GetAllGroups()
        {
            var groups = _db.Groups.ToList();
            return groups;
        }

        public List<UserGroup> GetAllUserGroups()
        {
            var userGroups = _db.UserGroup.ToList();
            return userGroups;
        }
        public List<Event> GetAllEvents()
        {
            var userEvents = _db.Events.ToList();
            return userEvents;
        }

        public string GetUserName()
        {
            string userName;
            userName = this.User.Identity.Name;
            return userName;
        }
    }

    
}
