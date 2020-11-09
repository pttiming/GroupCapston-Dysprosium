using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupCapstone.Data;
using GroupCapstone.Models;
using GroupCapstone.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupCapstone.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ApplicationDbContext _db;
        public GoogleService _google;

        public ChatController(UserManager<IdentityUser> userManager, ApplicationDbContext db, GoogleService google)
        {
            _userManager = userManager;
            _db = db;
            _google = google;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
            if (participant == null)
            {
                return RedirectToAction("Create", "Participants");
            }
            var groups = _db.UserGroup
                .Where(gp => gp.UserName == _userManager.GetUserName(User))
                .Join(_db.Groups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                        new UserGroupViewModel
                        {
                            UserName = ug.UserName,
                            GroupId = g.ID,
                            GroupName = g.GroupName
                        })
                .ToList();
            if (groups.Count == 0)
            {
                return RedirectToAction("IndexList", "Home");
            }
            ViewData["UserGroups"] = groups;
            // get all users      
            ViewData["Users"] = _userManager.Users;
            return View();
        }

        public async Task<IActionResult> Create(Event groupChatEvent)
        {
            groupChatEvent = await _google.GetGeoCode(groupChatEvent);
            groupChatEvent.Group = _db.Groups.Where(g => g.ID == groupChatEvent.GroupId).SingleOrDefault();
            _db.Events.Add(groupChatEvent);
            _db.SaveChanges();
            // Investigate Error
            return RedirectToAction("Index");
        }
    }
}
