using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
//using Microsoft.Web.Helpers;
using Recaptcha;
using MNF4.Utility;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
    public class EmailController : Controller
    {
        protected static string returnUrl = String.Empty;
        //
        // GET: /Email/

        public ActionResult Index()
        {
            return RedirectToAction("Question");
        }

        public ActionResult Question()
        {
            if (HttpContext.Request.UrlReferrer != null)
                returnUrl = HttpContext.Request.UrlReferrer.PathAndQuery;

            return View();
        }

        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        public ActionResult Send(EmailViewModel model, bool captchaValid, string captchaErrorMessage)
        {
            //string privateKey = WebConfigurationManager.AppSettings["ReCaptchaPrivateKey"];
            //if (RecaptchaControlMvc.Validate(privateKey) && (ModelState.IsValid))
            if (captchaValid && ModelState.IsValid)
            {
                try
                {
                    // model values passed in from form, extras below
                    model.returnUrl = returnUrl;
                    model.SubmitDate = DateTime.Now;

                    new Email().InboundMessage(model);
                }
                catch (Exception ex)
                {
                    base.ViewData["Exception"] = "Exception: " + ex.Message + " Stack Trace: " + ex.StackTrace;
                }

                return View("Thanks", model);
            }
            else
                return View("Question");

        }
    }
}
