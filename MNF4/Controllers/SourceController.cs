using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;

namespace MNF4.Controllers
{
    [Authorize(Roles = "mnf_admin")]
    public class SourceController : Controller
    {
        ISourceRepository sourceRepository;

        //
        // Dependency Injection enabled constructors
        public SourceController()
            : this(new SourceRepository()) {
        }

        public SourceController(ISourceRepository repository) {
            sourceRepository = repository;
        }

        //
        //  /Source
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        //
        //  /Source/List
        public ActionResult List()
        {
            IQueryable<Source> sources = sourceRepository.SourceList();
            return View(sources);
        }

        //
        // GET: /Source/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Source());
        }

        //
        // POST: /Source/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Source source)
        {
            if(ModelState.IsValid)
            {
                sourceRepository.InsertOrUpdate(source);
                sourceRepository.Save();

                return RedirectToAction("List");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            Source source = sourceRepository.Find(id);

            ViewBag.Name = source.Name;

            return View(source);

        }

        //
        // HTTP GET: /Source/Edit/1
        public ActionResult Edit(int id)
        {
            Source source = sourceRepository.Find(id);

            if (source == null)
                return View("Not Found");

            ViewData["Edit"] = source.Name;

            return View(source);
        }

        //
        // HTTP POST: /Source/Edit/1
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Source source = sourceRepository.Find(id);

            if (source == null)
                return View("Not Found");

            if (ModelState.IsValid)
            {
                source.Name = collection["Name"];
                source.Notes = collection["Notes"];
                source.StartDate = CheckDate(collection["StartDate"]);
                source.EndDate = CheckDate(collection["EndDate"]);

                sourceRepository.InsertOrUpdate(source);
                sourceRepository.Save();
            }
            else
            {
                return View(source);
            }
            return RedirectToAction("List");
        }

        //
        // HTTP GET: /Source/Delete/1
        public ActionResult Delete(int id)
        {
            Source source = sourceRepository.Find(id);

            if (source == null)
                return View("Not Found");

            ViewData["Delete?"] = source.Name;

            return View(source);
        }

        //
        // HTTP POST: /Source/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            Source source = sourceRepository.Find(id);

            if (source == null)
                return View("Not Found");

            ViewData["Deleted"] = source.Name;

            sourceRepository.Delete(id);
            sourceRepository.Save();

            return View("Deleted");
        }

        //
        // Takes a string date input and parses or returns null
        private DateTime? CheckDate(string date)
        {
            // date fields can be null if not entered
            DateTime dateTime;
            if (DateTime.TryParse(date, out dateTime))
                return dateTime;
            else
                return null;
        }
    }
}
