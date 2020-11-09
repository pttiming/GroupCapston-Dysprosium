using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupCapstone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GroupCapstone.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Create(Event groupChatEvent)
        {

            return RedirectToAction("Chat", "Index");
        }
    }
}
