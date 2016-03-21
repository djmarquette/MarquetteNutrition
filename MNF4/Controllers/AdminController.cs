using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;

namespace MNF4.Controllers
{
    [Authorize(Roles = "mnf_admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Appointments()
        {
            return View();
        }
    }
}
