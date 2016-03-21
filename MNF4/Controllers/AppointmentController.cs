using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;

namespace MNF4.Controllers
{
    [Authorize(Roles = "mnf_admin")]
    public class AppointmentController : Controller
    {
        private MNFdb db = new MNFdb();

        //
        // GET: /Appointment/

        public ActionResult Index()
        {
            var appointments = db.Appointments.Include(a => a.Client);
            return View("List", appointments.ToList());
        }

        //
        // GET: /Appointment/Details/5

        public ActionResult Details(int id = 0)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        //
        // GET: /Appointment/Create

        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName");
            return View();
        }

        //
        // POST: /Appointment/Create

        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", appointment.ClientID);
            return View(appointment);
        }

        //
        // GET: /Appointment/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", appointment.ClientID);
            return View(appointment);
        }

        //
        // POST: /Appointment/Edit/5

        [HttpPost]
        public ActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ID", "FirstName", appointment.ClientID);
            return View(appointment);
        }

        //
        // GET: /Appointment/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Appointment appointment = db.Appointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        //
        // POST: /Appointment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointments.Find(id);
            db.Appointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}