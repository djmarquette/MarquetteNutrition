using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.Models;
using MNF4.Utility;
using MNF4.ViewModels;
using System.Globalization;

namespace MNF4.Controllers
{
    public class ServicesController : Controller
    {
        //
        // GET: /Services/
        public ActionResult Index()
        {
            return View("Services");
        }

        #region Programs Views
        public ActionResult Programs()
        {
            return View("Programs");
        }

        public ActionResult Celiac()
        {
            return View("Programs/Celiac");
        }

        public ActionResult LEAP()
        {
            return View("Programs/Leap");
        }

        public ActionResult PCOS()
        {
            return View("Programs/PCOS");
        }

        public ActionResult Sports()
        {
            return View("Programs/Sports");
        }

        public ActionResult Vegetarian()
        {
            return View("Programs/Vegetarian");
        }

        public ActionResult Weight()
        {
            return View("Programs/Weight");
        }

        public ActionResult Wellness()
        {
            return View("Programs/Wellness");
        }

        public ActionResult Membership()
        {
            return View("Programs/Membership");
        }
        #endregion
        
        #region Other Services
        public ActionResult AddOns()
        {
            return View("AddOns");
        }

        public ActionResult FollowUp()
        {
            return View("FollowUp");
        }

        public ActionResult Seminars()
        {
            return View("Seminars");
        }

        public ActionResult Supplements()
        {
            return View("Supplements");
        }
        #endregion

    }
}
