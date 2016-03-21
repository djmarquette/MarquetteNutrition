using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNF4.Controllers
{
    [Authorize(Roles = "mnf_admin")]
    public class ReportsController : Controller
    {
        //
        // GET: /Reports/
        public ActionResult Index()
        {
            return View();
        }

    }
}
