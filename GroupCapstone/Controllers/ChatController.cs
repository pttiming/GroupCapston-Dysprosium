﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupCapstone.Data;
using GroupCapstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GroupCapstone.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GroupChatContext _GroupContext;
        public ApplicationDbContext _db;

        public ChatController(UserManager<IdentityUser> userManager, GroupChatContext context, ApplicationDbContext db)
        {
            _userManager = userManager;
            _GroupContext = context;
            _db = db;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var participant = _db.Participants.Where(e => e.IdentityUserId == userId).SingleOrDefault();
           if (participant == null)
            {
                return RedirectToAction("Create", "Participants");
            }
            var groups = _GroupContext.UserGroup
                .Where(gp => gp.UserName == _userManager.GetUserName(User))
                .Join(_GroupContext.Groups, ug => ug.GroupId, g => g.ID, (ug, g) =>
                        new UserGroupViewModel
                        {
                            UserName = ug.UserName,
                            GroupId = g.ID,
                            GroupName = g.GroupName
                        })
                .ToList();
            ViewData["UserGroups"] = groups;

            // get all users      
            ViewData["Users"] = _userManager.Users;
            return View();
        }
    }
}
