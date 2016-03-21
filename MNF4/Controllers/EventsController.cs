using System;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Models.Interfaces;
using MNF4.Models.Repositories;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        public EventsController() : this(new EventRepository()){}
        public EventsController(EventRepository repository)
        {
            _eventRepository = repository;
        }

        //
        // GET: /Events/
        public ActionResult Index()
        {
            var viewModel = new EventListViewModel();
            var vmList = viewModel.DisplayEvents();

            return View("DisplayEvents", vmList);
        }

#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult List()
        {
            var viewModel = new EventListViewModel();
            var vmList = viewModel.ListEvents();
            ViewData["Count"] = vmList.Count();

            return View("ListEvents", vmList);
        }

        //
        // GET: /Event/Details/5
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Details(int id = 0)
        {
            var viewModel = new EventFormViewModel(id);
            if (viewModel.ID == -1)
            {
                return HttpNotFound();
            }
            return View("EventDetails", viewModel);
        }

        //
        // GET: /Event/Create
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel();
            return View("CreateEvent", viewModel);
        }

        //
        // POST: /Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Create(EventFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("List");
                }
                else
                {
                    var errors = ModelState.Where(v => v.Value.Errors.Any()); 
                    throw new ModelValidationException("ModelState Error");
                }
            }
            catch (Exception ex)
            {
                ViewData["Exception"] = "Exception: " + ex.InnerException;
                return RedirectToAction("Create");  // not sure this gets reached in the event of an error
            }
            return RedirectToAction("Create");
        }

        //
        // GET: /Event/Edit/5
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Edit(int id = 0)
        {
            //MNFEvent mnfEvent = _eventRepository.Find(id);

            var viewModel = new EventFormViewModel(id);
            if (viewModel.ID == -1)
            {
                return HttpNotFound();
            }
            return View("EditEvent", viewModel);
        }

        //
        // POST: /Event/Edit/5
        [HttpPost]
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Edit(EventFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("List");
                }
                else
                {
                    var errors = ModelState.Where(v => v.Value.Errors.Any());
                    throw new ModelValidationException("ModelState Error");
                }
            }
            catch (Exception ex)
            {
                ViewData["Exception"] = "Exception: " + ex.Message + ex.InnerException + ex.StackTrace;
                return RedirectToAction("Edit");  // not sure this gets reached in the event of an error
            }
            return RedirectToAction("Edit");
        }

        //
        // GET: /Event/Delete/5
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult Delete(int id = 0)
        {
            var viewModel = new EventFormViewModel(id);
            if (viewModel.ID == -1)
            {
                return HttpNotFound();
            }
            return View("DeleteEvent", viewModel);
        }

        //
        // POST: /Event/Delete/5
        [HttpPost, ActionName("Delete")]
#if !DEBUG
        [Authorize(Roles = "mnf_admin")]
#endif
        public ActionResult DeleteConfirmed(int id)
        {
            _eventRepository.Delete(id);
            _eventRepository.Save();

            return RedirectToAction("List");
        }
    }
}
