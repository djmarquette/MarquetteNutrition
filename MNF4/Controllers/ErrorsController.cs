using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNF4.Controllers
{
    public class ErrorsController : Controller
    {
        //
        // GET: /Errors/
        public ActionResult Index()
        {
            return View("GeneralError");
        }

        public ActionResult General(Exception exception)
        {
            ViewData["ErrorMsg"] = exception.Message;
            return View("GeneralError");
        }

        public ActionResult Http404()
        {
            return View("Http404");
        }

        public ActionResult Http403()
        {
            return View("Http403");
        }

        public ActionResult Http500(Exception exception)
        {
            ViewData["ErrorMsg"] = exception.Message;
            return View("GeneralError");
        }
    }
}
