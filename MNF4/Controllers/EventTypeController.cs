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
#if (!DEBUG)
        [Authorize(Roles = "mnf_admin")]
#endif
    public class EventTypeController : Controller
    {
        private MNFdb db = new MNFdb();

        //
        // GET: /EventType/

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View("EventTypeList", db.EventTypes.ToList());
        }
        //
        // GET: /EventType/Details/5

        public ActionResult Details(int id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            return View("EventTypeDetails",eventtype);
        }

        //
        // GET: /EventType/Create

        public ActionResult Create()
        {
            return View("CreateEventType");
        }

        //
        // POST: /EventType/Create

        [HttpPost]
        public ActionResult Create(EventType eventtype)
        {
            if (ModelState.IsValid)
            {
                db.EventTypes.Add(eventtype);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("CreateEventType",eventtype);
        }

        //
        // GET: /EventType/Edit/5

        public ActionResult Edit(int id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            return View("EditEventType",eventtype);
        }

        //
        // POST: /EventType/Edit/5

        [HttpPost]
        public ActionResult Edit(EventType eventtype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventtype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("EditEventType",eventtype);
        }

        //
        // GET: /EventType/Delete/5

        public ActionResult Delete(int id = 0)
        {
            EventType eventtype = db.EventTypes.Find(id);
            if (eventtype == null)
            {
                return HttpNotFound();
            }
            return View("DeleteEventType",eventtype);
        }

        //
        // POST: /EventType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            EventType eventtype = db.EventTypes.Find(id);
            db.EventTypes.Remove(eventtype);
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