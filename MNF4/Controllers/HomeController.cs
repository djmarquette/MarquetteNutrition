using System;
using System.Web.Mvc;
using MNF4.ViewModels;


namespace MNF4.Controllers
{
    public class HomeController : Controller
    {
        private static OptInViewModel _vm;

        public ActionResult Closed()
        {
            return View();
        }

        public ActionResult Index()
        {
            _vm = new OptInViewModel
                {
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    EmailAddress = string.Empty,
                    oiSource = @"Restaurant Opt-In",
                    oiDocument = @"RestaurantMeals.pdf",
                    oiMessage = @"Sign up for our newsletter and receive a free report on restaurant meals!",
                    oiResult = string.Empty
                };

            return View("Index", _vm);
        }

        //
        // GET: /OptIn/ -- restaurant guide optIn
        public ActionResult OptIn(OptInViewModel viewModel)
        {
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

        public PartialViewResult ResetOptIn()
        {
            _vm.oiMessage = string.Empty;
            _vm.oiResult = @"Check your inbox for your requested free document "
                        + "(email will come from 'mail@marquettenutrition.com')";

            return PartialView("_OptInPartial", _vm);
        }
    }
}
