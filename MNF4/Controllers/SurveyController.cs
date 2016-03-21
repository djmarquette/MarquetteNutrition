using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNF4.Controllers
{
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StrategySession()
        {
            return View("StrategySession");
        }

        public ActionResult LEAPPrescreen()
        {
            return View("LeapPrescreen");
        }

    }
}
