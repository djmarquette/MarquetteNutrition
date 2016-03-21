using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using MNF4.ViewModels;


namespace MNF4.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Index()
        {
            return View("About");
        }

        public ActionResult Chris()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View("FAQ");
        }

        public ActionResult Location()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult Media()
        {
            return View();
        }

        //public ActionResult Privacy()
        //{
        //    return View();
        //}

        public ActionResult Blog()
        {
            BlogViewModel vm = new BlogViewModel();

            return View("Blog", vm.rssData);
        }

//        public ActionResult Blog2()
//        {
//            var vm = new BlogViewModel();
//
//            ViewData["wpBlogUrl"] = vm.wpUrl.Replace("/atom.xml", "");
//            ViewData["bsBlogUrl"] = vm.bsUrl.Replace("/atom.xml", "");
//
//            return View("Blog2", vm);
//        }
    }
}
