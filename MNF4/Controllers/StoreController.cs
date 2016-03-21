using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MNF4.ViewModels;

namespace MNF4.Controllers
{
#if !DEBUG
    [RequireHttps]
#endif
    public class StoreController : Controller
    {
        //
        // GET: /Store/
        StoreViewModel model = new StoreViewModel();

        public ActionResult Index()
        {
            return View("Storefront", model);
        }

        public ActionResult Product(WebinarViewModel webinarModel)
        {
            model.StorePage += webinarModel.StorePage;  // should pass whatever webinar passes in
            return View("Storefront", model);           // build and add a string to include the correct product
        }

    }
}
