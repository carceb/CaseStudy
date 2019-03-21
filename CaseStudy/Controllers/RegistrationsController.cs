using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaseStudy.DAL;
using CaseStudy.Models;

namespace CaseStudy.Controllers
{
    public class RegistrationsController : Controller
    {
        private CaseStudyContext db = new CaseStudyContext();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.Participants).Include(r => r.Sessions);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantName");
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "SessionName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationID,ParticipantID,SessionID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                if(!IsRegistered(registration.SessionID, registration.ParticipantID))
                {
                    if(!IsFull(registration.SessionID))
                    {
                        db.Registrations.Add(registration);
                        db.SaveChanges();
                        UpdateOcupancy(registration.SessionID);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = string.Format("Session is full");
                        return View("~/Views/Message.cshtml");
                    }
                }
                else
                {
                    ViewBag.Message = string.Format("User is registered");
                    return View("~/Views/Message.cshtml");
                }
            }

            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantName", registration.ParticipantID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "SessionName", registration.SessionID);
            return View(registration);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantName", registration.ParticipantID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "SessionName", registration.SessionID);
            return View(registration);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationID,ParticipantID,SessionID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParticipantID = new SelectList(db.Participants, "ParticipantID", "ParticipantName", registration.ParticipantID);
            ViewBag.SessionID = new SelectList(db.Sessions, "SessionID", "SessionName", registration.SessionID);
            return View(registration);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public bool IsRegistered(int sessionID, int participantID)
        {
            var total = db.Registrations.Where(x => x.SessionID == sessionID && x.ParticipantID == participantID).ToList();
            if(total.Count > 0)
            {
                return true;
            }

            return false;
        }
        public bool IsFull(int sessionID)
        {
            var total = db.Sessions.Where(x => x.SessionID == sessionID && x.MaximumCapacity == 0).ToList();
            
            if (total.Count == 1)
            {
                return true;
            }

            return false;
        }
        public void UpdateOcupancy(int sessionID)
        {
            var total = db.Sessions.Where(x => x.SessionID == sessionID).ToList();
            var maxC = 0;
            foreach (var t in total)
            {
                maxC = t.MaximumCapacity;
            }
            if(maxC == 0)
            {
                maxC = 1;
            }
            else
            {
                maxC --;
            }
            var session = new Session() { SessionID = sessionID, MaximumCapacity = maxC  };
            
            using (var db = new CaseStudyContext())
            {
                db.Sessions.Attach(session);
                db.Entry(session).Property(x => x.MaximumCapacity).IsModified = true;
                db.SaveChanges();
            }
        }
    }
}
