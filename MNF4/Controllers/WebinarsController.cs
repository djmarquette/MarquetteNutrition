using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Helpers;
using MNF4.Models;
using MNF4.ViewModels;
using MNF4.Utility;

namespace MNF4.Controllers
{
    public class WebinarsController : Controller
    {
        readonly IContactRepository contactRepository;

        //
        // Dependency Injection enabled constructors
        public WebinarsController()
            : this(new ContactRepository()) {
        }

        public WebinarsController(IContactRepository repository)
        {
            contactRepository = repository;
        }

        //
        // GET: /Webinars/
        public ActionResult Index()
        {
            return View("PCOS/5StepOptIn");
        }

        //
        // GET: /PCOS - optIn page
        public ActionResult PCOSFreeWebinar()
        {
            return View("PCOS/5StepOptIn");
        }

        //
        // Register for Webinar - Ajax partial postback
        public ActionResult Register(OptInViewModel viewModel)
        {
            var sourceRepository = new SourceRepository();

            if (ModelState.IsValid && string.IsNullOrEmpty(viewModel.botCheck))
            {
                try
                {
                    var contact = new Contact
                        {
                            FirstName = viewModel.FirstName,
                            LastName = viewModel.LastName,
                            EmailAddress = viewModel.EmailAddress,
                            SubmitDate = DateTime.Now,
                            optIn = true
                        };

                    var source = sourceRepository.FindByName("PCOS Webinar Opt-In");
                    contact.SourceID = source.ID;
                    contact.Comments = source.Notes;

                    contactRepository.InsertOrUpdate(contact);
                    contactRepository.Save();

                    var email = new Email();
                    var emailViewModel = FormatEmail(contact);
                    email.InboundMessage(emailViewModel);

                    string docPath = Server.MapPath("/Content/Documents/PCOSWebinarInstructions.htm");
                    var instructions = MvcHtmlString.Create(System.IO.File.ReadAllText(docPath, System.Text.Encoding.UTF8));
                    email.SendWebinarInstructions(emailViewModel, instructions);
                    
                }
                catch (Exception ex)
                {
                    base.ViewData["Exception"] = "Exception: " + ex.Message + " Stack Trace: " + ex.StackTrace;
                }
                return RedirectToAction("Thanks");
            }
            else
                return PartialView("_WebinarOptInPartial");
        }

        public PartialViewResult Thanks()
        {
            ViewData["ConfirmationMsg"] = 
                @"Thank you for registering.  We have received your information and you will be receiving an email 
                    with instructions in the next few days.  Please add mail@marquettenutrition.com to your 
                    safe senders list so you won't miss it.";

            return PartialView("_WebinarOptInPartial");
        }


        //
        // GET: /PCOS/5Step - sales page
        [HttpGet]
        public ActionResult PCOS5StepFormula()
        {
            return View("PCOS/5StepFormula");
        }

        [HttpPost]
        //public ActionResult PCOS5StepFormula(ProductViewModel model)
        //{
        //    if (model.Command.StartsWith("$699"))    // handles "$699 (after coupon)" case
        //    {
        //        // setup page with single payment
        //        model.StorePage = "/index.php?p=product&id=17";
        //    }
        //    else
        //    {
        //        // setup  new product page for multiple payments
        //        model.StorePage = "/index.php?p=product&id=17";
        //    }
        //    return RedirectToAction("Product", "MNFStore", model);
        //}

        private EmailViewModel FormatEmail(Contact contact)
        {

            var model = new EmailViewModel
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    EmailAddress = contact.EmailAddress,
                    Comments = contact.Comments,
                    Subject = "PCOS Webinar Opt-In Submission",
                    SentFrom = "PCOS 5 Step Opt-In page"
                };

            return model;
        }
    }
}

