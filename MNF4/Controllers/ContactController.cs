using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Recaptcha;
using MNF4.Models;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class ContactController : Controller
    {
        readonly IContactRepository _contactRepository;
        readonly ISourceRepository _sourceRepository;

        //
        // Dependency Injection enabled constructors
        public ContactController()
            : this(new ContactRepository())
        {
        }

        public ContactController(IContactRepository repository)
        {
            _contactRepository = repository;
            _sourceRepository = new SourceRepository();
        }

        //
        // GET: /Contact/
        public ActionResult Index()
        {
            return View("CreateContact");
        }

        [Authorize(Roles = "mnf_admin")]
        public ActionResult List()
        {
            var vm = new ContactListViewModel();

            List<ContactListViewModel> list = vm.ListContacts();
            ViewData["Count"] = list.Count();
            
            return View(list);
        }

        [Authorize(Roles = "mnf_admin")]
        public ViewResult ExportToExcel()
        {
            var vm = new ContactListViewModel();
            List<ContactListViewModel> list = vm.ListContacts();

            Response.Clear();
            Response.BufferOutput = true;
            Response.AddHeader("Content-Disposition", "attachment; filename=ContactList.xls");
            Response.ContentType = "application/vnd.ms-excel";

            return View(list);
        }

        [Authorize(Roles = "mnf_admin")]
        public ActionResult Search(string q)
        {
            var vm = new ContactListViewModel();

            List<ContactListViewModel> vmList = vm.ListContacts(q);
            ViewData["Count"] = vmList.Count();

            if (Request.IsAjaxRequest())
                return PartialView("_ContactListPartial", vmList);

            return View("List", vmList);
        }

        [Authorize(Roles = "mnf_admin")]
        public ActionResult Details(int id)
        {
            Contact contact = _contactRepository.Find(id);

            ViewBag.Source = _sourceRepository.Find(contact.SourceID);
            ViewBag.Name = contact.FirstName + " " + contact.LastName;

            return View(contact);
        }

        //
        // GET: /Contact/
        [HttpGet]
        public ActionResult Create()
        {

            return View("CreateContact" ,new ContactFormViewModel());
        }

        //
        // POST: /Contact/Create
        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(ContactFormViewModel viewModel, bool captchaValid, string captchaErrorMessage)
        {
            try
            {
                //string privateKey = WebConfigurationManager.AppSettings["ReCaptchaPrivateKey"];
                //if (ReCaptcha.Validate(privateKey) && ModelState.IsValid)

                if (!captchaValid)
                    if (!HttpContext.Request.Browser.IsMobileDevice)
                        ModelState.AddModelError("captcha", captchaErrorMessage);

                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("Thanks");
                }
                return View("CreateContact");
            }
            catch (Exception ex)
            {
#if DEBUG
                ViewData["Exception"] = "Exception: " + ex.Message + " ";
                if (ex.InnerException != null)
                {
                    string innerAddOn = " " + ex.InnerException;
                    ViewData["Exception"] += innerAddOn;
                }
#endif
                return View("CreateContact");
            }
        }

        //
        // POST: /Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MobileSubmit(ContactFormViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (viewModel.Save(viewModel))
                        return RedirectToAction("Thanks");
                }
                return View("CreateContact");
            }
            catch (Exception ex)
            {
#if DEBUG
                ViewData["Exception"] = "Exception: " + ex.Message + " ";
                if (ex.InnerException != null)
                {
                    string innerAddOn = " " + ex.InnerException;
                    ViewData["Exception"] += innerAddOn;
                }
#endif
                return View("CreateContact");
            }
        }

        //
        // HTTP GET: /Contact/Delete/1
        [Authorize(Roles = "mnf_admin")]
        public ActionResult Delete(int id)
        {
            Contact contact = _contactRepository.Find(id);

            if (contact == null)
                return View("NotFound");

            ViewData["Delete?"] = contact.FirstName + " " + contact.LastName;
            ViewBag.Source = _sourceRepository.Find(contact.SourceID);

            return View("DeleteContact", contact);
        }

        // 
        // HTTP POST: /Contact/Delete/1
        [HttpPost, Authorize(Roles = "mnf_admin")]
        public ActionResult Delete(int id, string confirmButton)
        {
            Contact contact = _contactRepository.Find(id);

            if (contact == null)
                return View("NotFound");

            ViewData["Deleted"] = contact.FirstName + " " + contact.LastName;

            _contactRepository.Delete(id);
            _contactRepository.Save();

            return View("Deleted");
        }

        //
        // HTTP GET: /Contact/Edit/1
        [Authorize(Roles = "mnf_admin")]
        public ActionResult Edit(int id)
        {
            Contact contact = _contactRepository.Find(id);

            if (contact == null)
            {
                ViewBag.RecordType = "Contact";
                return View("NotFound");
            }

            ViewData["Edit"] = contact.FirstName + " " + contact.LastName;

            var viewModel = new ContactFormViewModel(contact.SourceID)
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = contact.Address,
                    City = contact.City,
                    State = contact.State,
                    ZipCode = contact.ZipCode,
                    EmailAddress = contact.EmailAddress,
                    optIn = contact.optIn,
                    Phone = contact.Phone,
                    Comments = contact.Comments,
                    SourceID = contact.SourceID,
                    Contacted = contact.Contacted,
                    ContactNotes = contact.ContactNotes
                };

            return View(viewModel);
        }

        //
        // HTTP POST: /Contact/Edit/1
        [HttpPost, Authorize(Roles = "mnf_admin")]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Contact contact = _contactRepository.Find(id);

            if (contact == null)
            {
                ViewBag.RecordType = "Contact";
                return View("NotFound");
            }

            if (ModelState.IsValid)
            {
                contact.FirstName = collection["FirstName"];
                contact.LastName = collection["LastName"];
                contact.Address = collection["Address"];
                contact.City = collection["City"];
                contact.State = collection["State"];
                contact.ZipCode = collection["ZipCode"];
                contact.EmailAddress = collection["EmailAddress"];
                contact.Phone = collection["Phone"];
                contact.Comments = collection["Comments"];
                contact.optIn = collection["optIn"].Contains("true");   //looking for [true],[false]
                contact.SourceID = int.Parse(collection["SourceID"]);
                contact.Contacted = collection["Contacted"].Contains("true");
                contact.ContactNotes = collection["ContactNotes"];

                _contactRepository.Save();
                return RedirectToAction("Details", new { id = contact.ID });
            }
            return View(new ContactFormViewModel());
        }

        //
        // GET: /Contact/Thanks -- thank you message for submitting contact form
        public ActionResult Thanks()
        {
            return View();
        }
    }
}