using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupCapstone.Data;
using GroupCapstone.Models;
using GroupCapstone.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GroupCapstone.Controllers
{
    public class ParticipantsController : Controller
    {
        public GoogleService _google;
        public ApplicationDbContext _db;

        public ParticipantsController(ApplicationDbContext db, GoogleService google)
        {
            _google = google;
            _db = db;
        }
        // GET: ParticipantsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ParticipantsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParticipantsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParticipantsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Participant participant)
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                participant.IdentityUserId = userId;
                await _google.GetGeoCode(participant);
                _db.Add(participant);
                _db.SaveChanges();
                return RedirectToAction("Index", "chat");
            }
            catch
            {
                return View();
            }
        }

        // GET: ParticipantsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParticipantsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParticipantsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParticipantsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
