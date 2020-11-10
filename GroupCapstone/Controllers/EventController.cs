using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            eventUserGroupViewModel.EventParticipants = GetAllEventParticipants();
            eventUserGroupViewModel.Participants = GetAllParticipants();
            eventUserGroupViewModel.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            eventUserGroupViewModel.Filtered = GetAllEvents();

            return View(eventUserGroupViewModel);
        }

        public List<Event> GetAvailableEvents()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            int participantId = participant.Id;
            var eventsToExclude = _db.EventParticipants.Where(ep => ep.ParticipantId == participantId).ToList();
            var events = GetAllEvents();
            List<Event> filteredEvents = events;
            for (int i = 0; i < events.Count; i++)
            {
                
                foreach(EventParticipants ep in eventsToExclude)
                {
                    if (ep.EventId == events[i].Id)
                    {
                        filteredEvents.Remove(events[i]);
                    }
                }
            }

            return filteredEvents;
        }
        public List<Participant> GetAllParticipants()
        {
            var ep = _db.Participants.ToList();
            return ep;
        }
        public List<EventParticipants> GetAllEventParticipants()
        {
            var ep = _db.EventParticipants.ToList();
            return ep;
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

        public IActionResult AttendEvent(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            EventParticipants ep = new EventParticipants();
            ep.EventId = id;
            ep.ParticipantId = participant.Id;

            _db.Add(ep);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult LeaveEvent(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            EventParticipants ep = _db.EventParticipants.Where(p => p.ParticipantId == participant.Id).Where(e => e.EventId == id).SingleOrDefault();

            _db.Remove(ep);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }

    
}
