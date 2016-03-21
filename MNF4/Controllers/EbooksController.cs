using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MNF4.Controllers
{
    public class ebooksController : Controller
    {
        //
        // GET: /ebooks/

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult PCOS()
        {
            return View("PCOS");
        }

        public ActionResult Celiac()
        {
            ViewBag.Price = "24.95";
            return View("Celiac");
        }

        // Readers of the Celiac Crash Course free ebook will have this URL for cheaper book
        public ActionResult CeliacCC()
        {
            ViewBag.Price = "14.95";
            return View("Celiac");
        }

    }
}
