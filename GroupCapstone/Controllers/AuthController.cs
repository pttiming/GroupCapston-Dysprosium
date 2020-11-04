using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupCapstone.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PusherServer;

namespace GroupCapstone.Controllers
{
    public class AuthController : Controller
    {
        private readonly GroupChatContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult ChannelAuth(string channel_name, string socket_id)
        {
            int group_id;
            if (!User.Identity.IsAuthenticated)
            {
                return new ContentResult { Content = "Access forbidden", ContentType = "application/json" };
            }

            try
            {
                group_id = Int32.Parse(channel_name.Replace("private-", ""));
            }
            catch (FormatException e)
            {
                return Json(new { Content = e.Message });
            }

            var IsInChannel = _context.UserGroup
                                      .Where(gb => gb.GroupId == group_id
                                            && gb.UserName == _userManager.GetUserName(User))
                                      .Count();

            if (IsInChannel > 0)
            {
                var options = new PusherOptions
                {
                    Cluster = API_KEYS.pusherAppCluster,
                    Encrypted = true
                };
                var pusher = new Pusher(
                    API_KEYS.pusherAppId,
                    API_KEYS.pusherAppKey,
                    API_KEYS.pusherAppSecret,
                    options
                );

                var auth = pusher.Authenticate(channel_name, socket_id).ToJson();
                return new ContentResult { Content = auth, ContentType = "application/json" };
            }
            return new ContentResult { Content = "Access forbidden", ContentType = "application/json" };
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
