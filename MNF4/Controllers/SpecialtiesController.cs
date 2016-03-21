using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Utility;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class SpecialtiesController : Controller
    {
        private static OptInViewModel _vm = new OptInViewModel();
        //
        // GET: /Specialties/
        public ActionResult Index()
        {
            return View("Specialties");
        }

        public ActionResult Celiac()
        {
            _vm.oiMessage = @"Download our FREE 'Celiac Crash Course' ebook by signing up for our newsletter!";
            _vm.oiResult = string.Empty;

            return View(_vm);
        }

        public ActionResult FoodReactions()
        {
            return View();
        }

        public ActionResult PCOS()
        {
            _vm.oiMessage = @"Sign up for our newsletter and receive a FREE PCOS resource guide including who your "
                            + "treatment team members should be and more!";
            _vm.oiResult = string.Empty;

            return View(_vm);
        }

        public ActionResult Sports()
        {
            _vm.oiMessage = @"Want some basic endurance sports nutrition info, including a sample menu?  Enter your "
                            + "name and email in the box below to receive a FREE copy of the Endurance Athlete Nutrition Guide.  You will also receive a complimentary subscription to our monthly newsletter.";
            _vm.oiResult = string.Empty;

            return View(_vm);
        }

        public ActionResult Vegetarian()
        {
            _vm.oiMessage = @"Enter your name and email here to receive a FREE guide on vitamin B12. You will also "
                            + "receive a complimentary subscription to our monthly newsletter.";
            _vm.oiResult = string.Empty;

            return View(_vm);
        }

        public ActionResult Weight()
        {
            _vm.oiMessage = @"For more information, enter your name and email below to get a handout on nutraMetrix "
                            + "Gene SNP and how it can improve your health.  You will also receive a complimentary "
                            + "subscription to our monthly newsletter.";
            _vm.oiResult = string.Empty;

            return View(_vm);
        }

        #region OptIn processing
        //
        // GET: /OptIn/ -- restaurant guide optIn
        public Object OptIn(OptInViewModel viewModel)
        {
            _vm.FirstName = string.Empty;
            _vm.LastName = string.Empty;
            _vm.EmailAddress = string.Empty;

            switch (this.Request.UrlReferrer.Segments[2])
            {
                case "Celiac":
                    _vm.oiSource = "CeliacCC Opt-In";
                    _vm.oiDocument = "CeliacCrashCourse.pdf";
                    break;
                case "PCOS":
                    _vm.oiSource = "PCOS Opt-In";
                    _vm.oiDocument = "PCOSresources.pdf";
                    break;
                case "Sports":
                    _vm.oiSource = "Sports Opt-In";
                    _vm.oiDocument = "EnduranceAthleteNutritionGuide.pdf";
                    break;
                case "Vegetarian":
                    _vm.oiSource = "Vegetarian Opt-In";
                    _vm.oiDocument = "B12-Consumer.pdf";
                    break;
                case "Weight":
                    _vm.oiSource = "Weight Management Opt-In";
                    _vm.oiDocument = "GeneSnp.pdf";
                    break;
            }

            try
            {
                if (ModelState.IsValid && string.IsNullOrEmpty(viewModel.botCheck))
                {
                    viewModel.Save(_vm);
                    return RedirectToAction("ResetOptIn");
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                ViewData["Exception"] = "Exception: " + ex.Message + " Stack Trace: " + ex.StackTrace;
#endif
            }
            return PartialView("_OptInPartial", _vm);
        }

        public ActionResult ResetOptIn()
        {
            _vm.oiMessage = string.Empty;
            _vm.oiResult = @"Check your inbox for your requested free document "
                        + "(email will come from 'mail@marquettenutrition.com')";

            return PartialView("_OptInPartial", _vm);
        }
        #endregion


    }
}
