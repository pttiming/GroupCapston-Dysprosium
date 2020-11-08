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
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private readonly GroupChatContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public MessageController(GroupChatContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("{group_id}")]
        public IEnumerable<Message> GetById(int group_id)
        {
            return _context.Message.Where(gb => gb.GroupId == group_id);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MessageViewModel message)
        {
            
            Message new_message = new Message { AddedBy = _userManager.GetUserName(User), message = message.message, GroupId = message.GroupId };

            _context.Message.Add(new_message);
            _context.SaveChanges();

            // Triggering an event when a new message is received - where to put?
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
            var result = await pusher.TriggerAsync(
                "private-" + message.GroupId,
                "new_message",
            new { new_message },
            new TriggerOptions() { SocketId = message.SocketId });

            return new ObjectResult(new { status = "success", data = new_message });
            
        }
    }
}
